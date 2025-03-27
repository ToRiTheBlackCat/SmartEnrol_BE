using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
namespace SmartEnrol.Infrastructure
{
    public interface IChatService
    {
        Task<string> GenerateResponse(string userInput);
    }

    public class ChatService : IChatService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly QueryRewrite _queryRewrite;
        private readonly QueryRouting _queryRouting;
        private readonly QueryConstruction _queryConstruction;
        private readonly PostRetrieval _postRetrieval;
        public ChatService(IHttpContextAccessor httpContextAccessor, 
                            QueryRewrite queryRewrite,
                           QueryRouting queryRouting,
                           QueryConstruction queryConstruction,
                           PostRetrieval postRetrieval)
        {
            _httpContextAccessor = httpContextAccessor;
            _queryRewrite = queryRewrite;
            _queryRouting = queryRouting;
            _queryConstruction = queryConstruction;
            _postRetrieval = postRetrieval;
        }

        private List<string> ChatHistory
        {
            get
            {
                var session = _httpContextAccessor.HttpContext?.Session;
                var historyJson = session?.GetString("ChatHistory");
                return historyJson != null ? JsonSerializer.Deserialize<List<string>>(historyJson) : new List<string>();
            }
            set
            {
                var session = _httpContextAccessor.HttpContext?.Session;
                var json = JsonSerializer.Serialize(value);
                session?.SetString("ChatHistory", json);
            }
        }

        public async Task<string> GenerateResponse(string userInput)
        {
            var response = "";
            var chatHistory = ChatHistory;
            chatHistory.Add(userInput);
            string queryTranslated = await TranslateQuery(userInput);
            string queryRouted = await RouteQuery(queryTranslated);
            //if(queryRouted.Contains("general"))
            //{
            //    //response = await _postRetrieval.GenerateResponse(userInput, "None");    
            //    response = await _postRetrieval.ChatWithHistory(userInput, "None", chatHistory);
            //    chatHistory.Add(response);
            //    ChatHistory = chatHistory;
            //    return response;
            //}
            string documents = await ConstructQuery(queryTranslated,queryRouted);
            //response = await _postRetrieval.GenerateResponse(queryTranslated, documents);
            response = await _postRetrieval.ChatWithHistory(queryTranslated, documents, chatHistory);
            chatHistory.Add(response);
            ChatHistory = chatHistory;
            return response;
        }
        private async Task<string> TranslateQuery(string userInput)
        {
            return await _queryRewrite.ReWrite(userInput);

        }
        private async Task<string> RouteQuery(string queryTranslated)
        {
            return await _queryRouting.Route(queryTranslated);
        }

        private async Task<string> ConstructQuery(string userInput, string queryRouted)
        {
            return await _queryConstruction.GenerateQueryString(userInput, queryRouted);
        }

    }
}

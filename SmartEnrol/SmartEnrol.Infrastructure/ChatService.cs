using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using SmartEnrol.Services.Service;

namespace SmartEnrol.Infrastructure
{
    public interface IChatService
    {
        Task<string> GenerateResponse(string userInput, int accId);
        Task<string> Test(string userInput);
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
        private readonly RecommendationAnalyzer _analyzeRecommend;
        private readonly RecommendationService _reccServ;

        public ChatService(
            QueryRewrite queryRewrite, 
            QueryRouting queryRouting, 
            QueryConstruction queryConstruction, 
            PostRetrieval postRetrieval, 
            RecommendationAnalyzer analyzeRecommend, 
            RecommendationService reccServ)
        {
            _httpContextAccessor = httpContextAccessor;
            _queryRewrite = queryRewrite;
            _queryRouting = queryRouting;
            _queryConstruction = queryConstruction;
            _postRetrieval = postRetrieval;
            _analyzeRecommend = analyzeRecommend;
            _reccServ = reccServ;
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

        public async Task<string> GenerateResponse(string userInput, int accId)
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
            // General chat
            if(queryRouted.Contains("general"))
            {
                return await _postRetrieval.GenerateResponse(userInput, "None");                
            }
            // Related to documents chat
            string documents = await ConstructQuery(queryTranslated,queryRouted);
            //response = await _postRetrieval.GenerateResponse(queryTranslated, documents);
            response = await _postRetrieval.ChatWithHistory(queryTranslated, documents, chatHistory);
            chatHistory.Add(response);
            ChatHistory = chatHistory;
            return response;
            string response = await _postRetrieval.GenerateResponse(queryTranslated, documents);
            List<int> uniMajorsIdList = await _analyzeRecommend.GetRecommendations(response);
            if(uniMajorsIdList != null)
            {
                foreach (int id in uniMajorsIdList)
                {
                    await _reccServ.Create(id, accId, response);
                }
            }
            return response;
        }

        public async Task<string> Test(string userInput)
        {
            return await _analyzeRecommend.AnalyzeRecommendation(userInput);
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

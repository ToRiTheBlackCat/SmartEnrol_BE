using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace SmartEnrol.Infrastructure
{
    public interface IChatService
    {
        Task<string> GenerateResponse(string userInput, string? sessionID);
        string GetOrCreateSessionID();
        void DeleteSessionID(string sessionID);
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

        private ISession Session => _httpContextAccessor.HttpContext?.Session;
        public string GetOrCreateSessionID()
        {
            if (Session == null) return string.Empty;

            string sessionID = Session.GetString("SessionID");
            if (string.IsNullOrEmpty(sessionID))
            {
                sessionID = Guid.NewGuid().ToString();
                Session.SetString("SessionID", sessionID);
            }
            return sessionID;
        }

        public void DeleteSessionID(string sessionID)
        {
            if (Session == null) return;

            Session.Remove("SessionID");
            Session.Remove(sessionID);
        }

        public async Task<string> GenerateResponse(string userInput, string? sessionID)
        {
            var chatHistory = GetChatHistory(sessionID);
            chatHistory.Add(userInput);
            string queryTranslated = await TranslateQuery(userInput);
            string queryRouted = await RouteQuery(queryTranslated);
            string documents = await ConstructQuery(queryTranslated, queryRouted);
            var response = await _postRetrieval.ChatWithHistory(queryTranslated, documents, chatHistory);
            SaveChatHistory(sessionID, chatHistory);
            return response;
        }

        private List<string> GetChatHistory(string sessionID)
        {
            var historyJson = Session?.GetString(sessionID);
            return historyJson != null ? JsonSerializer.Deserialize<List<string>>(historyJson) : new List<string>();
        }

        private void SaveChatHistory(string sessionID, List<string> history)
        {
            var json = JsonSerializer.Serialize(history);
            Session?.SetString(sessionID, json);
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

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
        private readonly QueryRewrite _queryRewrite;
        private readonly QueryRouting _queryRouting;
        private readonly QueryConstruction _queryConstruction;
        private readonly PostRetrieval _postRetrieval;
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
            _queryRewrite = queryRewrite;
            _queryRouting = queryRouting;
            _queryConstruction = queryConstruction;
            _postRetrieval = postRetrieval;
            _analyzeRecommend = analyzeRecommend;
            _reccServ = reccServ;
        }

        public async Task<string> GenerateResponse(string userInput, int accId)
        {
            string queryTranslated = await TranslateQuery(userInput);
            string queryRouted = await RouteQuery(queryTranslated);
            // General chat
            if(queryRouted.Contains("general"))
            {
                return await _postRetrieval.GenerateResponse(userInput, "None");                
            }
            // Related to documents chat
            string documents = await ConstructQuery(queryTranslated,queryRouted);
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

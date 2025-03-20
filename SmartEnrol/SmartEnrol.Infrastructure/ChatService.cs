namespace SmartEnrol.Infrastructure
{
    public interface IChatService
    {
        Task<string> GenerateResponse(string userInput);
    }

    public class ChatService : IChatService
    {
        private readonly QueryRewrite _queryRewrite;
        private readonly QueryRouting _queryRouting;
        private readonly QueryConstruction _queryConstruction;
        private readonly PostRetrieval _postRetrieval;
        public ChatService(QueryRewrite queryRewrite,
                           QueryRouting queryRouting,
                           QueryConstruction queryConstruction,
                           PostRetrieval postRetrieval)
        {
            _queryRewrite = queryRewrite;
            _queryRouting = queryRouting;
            _queryConstruction = queryConstruction;
            _postRetrieval = postRetrieval;
        }
        public async Task<string> GenerateResponse(string userInput)
        {
            string queryTranslated = await TranslateQuery(userInput);
            string queryRouted = await RouteQuery(queryTranslated);
            if(queryRouted.Contains("general"))
            {
                return await _postRetrieval.GenerateResponse(userInput, "None");                
            }
            string documents = await ConstructQuery(queryTranslated,queryRouted);
            return await _postRetrieval.GenerateResponse(queryTranslated, documents);
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

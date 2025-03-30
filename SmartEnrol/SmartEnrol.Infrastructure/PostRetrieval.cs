using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;

namespace SmartEnrol.Infrastructure
{
    public class PostRetrieval
    {
        private readonly IConfiguration _configuration;
        private readonly List<Content> chatHistory = new();

        public PostRetrieval(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ChatWithHistory(string userInput, string data, List<string> history)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var googleAi = new GoogleAi(apiKey);
            var aiModel = googleAi.CreateGenerativeModel(GoogleAIModels.Gemini2Flash);
            var config = new GenerationConfig()
            {
                MaxOutputTokens = 800,
                Temperature = 0.5,
            };
            if (data.Contains("None"))
            {
                var prompt = $"Your name is SmartEnrol - You are an AI consultant assistant specialized in giving advice abouts admission method of universities, major in VietNam.You are created by Future Aim Company. If the user's question is within this scope, provide a detailed and accurate response. If the question is outside your scope, still try to answer as helpfully as possible by your own. However, if the user asks a greeting question such as 'hello', 'hi', 'xin chào', respond with the predefined reply you have been trained with: Xin chào tôi tên là SmartEnrol - Một trợ lý ảo được tạo ra để hỗ trợ việc tư vấn tuyển sinh đại học. Tôi có thể giúp gì cho bạn không?. Here is the question: {userInput} ";

                var instructions = " . Here are your instructions in how to response:" + "1. You must not use List, use paragraph." + "2. Avoid redundant phrasing or excessive elaboration." + "3. Stick to the key points relevant to the query." + "4. Remove phrases like \"It is important to note that...\" or \"As previously mentioned...\". Get straight to the point." + "5. Use clear, direct statements." + "6. Avoid multiple or extended explanations." + "7. Do not self-reference or apologize unnecessarily." + "8. Reply in VietNamese";
                chatHistory.Add(new Content(instructions, Roles.User));
                chatHistory.Add(new Content(prompt, Roles.User));
                foreach (var item in history)
                {
                    chatHistory.Add(new Content(item, Roles.User));
                }
                var chatSession = aiModel.StartChat(config: config, history: chatHistory);
                var response = await chatSession.GenerateContentAsync(userInput);
                if (response == null)
                {
                    return "No valid response from Gemini.";
                }

                chatHistory.Add(new Content(response.ToString(), Roles.Model));
                history.Add(response.ToString());

                return response.ToString();
            }
            else
            {
                var prompts = $"Your name is SmartEnrol - You are an AI consultant assistant specialized in giving advice abouts admission method of universities, major in VietNam.You are created by Future Aim Company. If the user's question is within this scope, provide a detailed and accurate response. If the question is outside your scope, still try to answer as helpfully as possible by your own. However, if the user asks a greeting question such as 'hello', 'hi', 'xin chào', respond with the predefined reply you have been trained with: Xin chào tôi tên là SmartEnrol - Một trợ lý ảo được tạo ra để hỗ trợ việc tư vấn tuyển sinh đại học. Tôi có thể giúp gì cho bạn không?. You will receive a user question and revelant data about that question but if you receive the user input + None which means that question is out of your scope  - then you  can answer with your knowledge, here are the data: {data}, and if user request for more information about universities, majors - which not contains in the data then you just can answer it on your own, not need to be so specific because it is not your mission and you need to give reference to what you answer like link,.. But if user continue chit chat just please provide a continuously answer for them until they start a new conversation. You will need to answer all question in VIETNAMESE because you are created to services student in VietNamese. Help them answer their question and just make the shortest and clearest answer.  And here is the input of the user: {userInput}";
                var instructions = " . Here are your instructions in how to response:" + "1. You must not use List, use paragraph." + "2. Avoid redundant phrasing or excessive elaboration." + "3. Stick to the key points relevant to the query." + "4. Remove phrases like \"It is important to note that...\" or \"As previously mentioned...\". Get straight to the point." + "5. Use clear, direct statements." + "6. Avoid multiple or extended explanations." + "7. Do not self-reference or apologize unnecessarily." + "8. If you lack information from context then search on your owns." + "9. Reply in VietNamese";

                chatHistory.Add(new Content(instructions, Roles.User));
                chatHistory.Add(new Content(prompts, Roles.User));

                foreach (var item in history)
                {
                    chatHistory.Add(new Content(item, Roles.User));
                }

                var chatSession = aiModel.StartChat(config: config, history: chatHistory);

                var response = await chatSession.GenerateContentAsync(userInput);
                if (response == null)
                {
                    return "No valid response from Gemini.";
                }

                chatHistory.Add(new Content(response.ToString(), Roles.Model));
                history.Add(response.ToString());

                return response.ToString();
            }
        }
    }
}

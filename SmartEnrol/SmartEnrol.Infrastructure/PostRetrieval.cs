using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SmartEnrol.Repositories.Models;
using GenerativeAI;
using GenerativeAI.Types;

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

        public async Task<string> GenerateResponse(string userInput, string data)
        {
            if (data.Contains("None"))
            {
                var prompt = $"Your name is SmartEnrol - an consultant AI Agent, you are created to help giving advice abouts admission method of universities, major in VietNam. When user say Hello or Xin chào to you then the introduction that you need to answer and cannot answer differently is: Xin chào tôi tên là SmartEnrol - Một trợ lý ảo được tạo ra để hỗ trợ việc tư vấn tuyển sinh đại học. Tôi có thể giúp gì cho bạn không?";
                var apiKey = _configuration["Gemini:ApiKey"];
                var googleAi = new GoogleAi(apiKey);
                var aiModel = googleAi.CreateGenerativeModel(GoogleAIModels.Gemini2Flash);
                var config = new GenerationConfig()
                {
                    MaxOutputTokens = 800,
                    Temperature = 0.5,
                };
                if (!chatHistory.Any())
                {
                    var instructions = " . Here are your instructions in how to response:" + "1. You must not use List, use paragraph." + "2. Avoid redundant phrasing or excessive elaboration." + "3. Stick to the key points relevant to the query." + "4. Remove phrases like \"It is important to note that...\" or \"As previously mentioned...\". Get straight to the point." + "5. Use clear, direct statements." + "6. Avoid multiple or extended explanations." + "7. Do not self-reference or apologize unnecessarily." + "8. If you lack information from context then search the internet." + "9. Reply in VietNamese";
                    chatHistory.Add(new Content(instructions, Roles.User));
                    chatHistory.Add(new Content(prompt, Roles.User));
                }
                var chatSession = aiModel.StartChat(config: config, history: chatHistory);
                var response = await chatSession.GenerateContentAsync(userInput);
                if (response == null)
                {
                    return "No valid response from Gemini.";
                }
                return response.ToString();
            }
            else
            {
                var apiKey = _configuration["Gemini:ApiKey"];
                var googleAi = new GoogleAi(apiKey);
                var aiModel = googleAi.CreateGenerativeModel(GoogleAIModels.Gemini2Flash);
                var config = new GenerationConfig()
                {
                    MaxOutputTokens = 800,
                    Temperature = 0.5,
                };
                //var urlPath = _configuration["Gemini:ApiUrl"];
                //var apiKey = _configuration["Gemini:ApiKey"];
                //var geminiUrl = urlPath + apiKey;

                //using HttpClient client = new HttpClient();
                //var sqlScriptTxt = await ReadScriptFromFileAsync();
                // sqlScriptTxt += "This is my DbContext class of my Sql Server Database. I will explain carefully for you to understand: University will contain the information of universities. UniMajor will contains the majors that universities teach; Major is all the majors that provided by the government in VietNam; The AdmissionMethodOfMajor will contains the admission methods that apply for that majors; AdmissionMethodOfUni will contain all the admission methods that universities have in that year; Area is the cities or provinces that students of universities are located; CharacteristicOfMajor are all the characteristics that suitable for that majors; Characteristic is the list of characteristic name;  CharacteristicOfStudent is when student login if they give their characteristic or hobby or what they like base on that to specify which characteristic are they; Others tables you can read to know. Caution that you cannot provide the Entities or fields of Entities that not exist in my DbContext and all the data in my Database is VietNamese but all fields are in English but with any fields that about name like major name, method name, university name,... just use contain not need exactly equal when compare. To be sure after give me an answer please scan that to compare with my DbContext if not correct please update that.";
                //sqlScriptTxt += "You are an adviser. Here are your instruction: 1.You must condense your answer into about 800 words. 2. You will not use list, always in paragraph. 3. Be succinct. 4. You dont give me conclusion. 5. You must follow the provided docs if you see the query is related to it. 6. You dont give summary or conclusion again. 7. Your answer must be short to fit about 800 tokens. 8. DO not include the history of the previous answer into the new answer. 9. Use the history of previous queries to answer but not inlcude them in when answer. 10. Keep your answer short";
                var prompts = $"Your name is SmartEnrol - an consultant AI Agent, you are created to help giving advice abouts admission method of universities, major in VietNam. When user say Hello or Xin chào to you then the introduction that you need to answer and cannot answer differently is: Xin chào tôi tên là SmartEnrol - Một trợ lý ảo được tạo ra để hỗ trợ việc tư vấn tuyển sinh đại học. Tôi có thể giúp gì cho bạn không? You will receive a user question and revelant data about that question but if you receive the user input + none which means that question is out of your scope  - then you  can answer with your knowledge., here are data: {data}, or user request for more information about universities, majors - which not contains in the data then you just can answer it on your own, not need to be so specific because it is not your mission and you need to give reference to what you answer like link,.. But if user continue chit chat just please provide a continuously answer for them until they start a new conversation. You will need to answer all question in VIETNAMESE because you are created to services student in VietNamese. Help them answer their question and just make the shortest and clearest answer.  And here is the input of the user: {userInput}";
                if (!chatHistory.Any())
                {
                    var instructions = " . Here are your instructions in how to response:" + "1. You must not use List, use paragraph." + "2. Avoid redundant phrasing or excessive elaboration." + "3. Stick to the key points relevant to the query." + "4. Remove phrases like \"It is important to note that...\" or \"As previously mentioned...\". Get straight to the point." + "5. Use clear, direct statements." + "6. Avoid multiple or extended explanations." + "7. Do not self-reference or apologize unnecessarily." + "8. If you lack information from context then search on your owns." + "9. Reply in VietNamese";
                    chatHistory.Add(new Content(instructions, Roles.User));
                }

                chatHistory.Add(new Content(prompts, Roles.User));

                var chatSession = aiModel.StartChat(config: config, history: chatHistory);
                var response = await chatSession.GenerateContentAsync(userInput);

                if (response == null)
                {
                    return "No valid response from Gemini.";
                }
                chatHistory.Add(new Content(response.ToString(), Roles.Model));

                return response.ToString();
            }

        }
    }
}

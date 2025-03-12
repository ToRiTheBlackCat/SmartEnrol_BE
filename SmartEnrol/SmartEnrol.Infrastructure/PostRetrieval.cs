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

        SmartEnrolContext context;
        public PostRetrieval(IConfiguration configuration, SmartEnrolContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        public async Task<string> GenerateResponse(string userInput, string data)
        {
            var outputQuery = await CallGoogleApi(userInput, data);
            return outputQuery;
        }

        private async Task<string> CallGoogleApi(string userInput, string data)
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
            var prompts = $"Your name is SmartEnrol and you were created by TriNHM, you are created to become a consultant agent that help give advice about admission method of universities in VietName but if the user chat anything that out of your scopes or request for more information about universities, majors,..., you can answer it with your knowledge and if user continure to asking please provide a continuously answer for them until they start a new conversation.Help them answer their question just make the shortest and clearest answer. These are revelant data about user question: {data} Here is the input of the user: ";
            var instructions = " . Here are your instructions in how to response:" + "1. You must not use List, use paragraph." + "2. Avoid redundant phrasing or excessive elaboration." + "3. Do not introduce unrelated details or make assumptions outside the given context. Stick to the key points relevant to the query." + "4. Remove phrases like \"It is important to note that...\" or \"As previously mentioned...\". Get straight to the point." + "5. Use clear, direct statements." + "6. Avoid multiple or extended explanations." + "7. Do not self-reference or apologize unnecessarily."+ "8. If you lack information from context then search on your owns." + "9. Reply in VietNamese";
            var history = new List<Content>
            {
                new Content(instructions, Roles.User),
                new Content(prompts, Roles.User),
            };

            var chatSession = aiModel.StartChat(config: config, history: history);
            var response = await chatSession.GenerateContentAsync(prompts);
            if (response == null)
            {
                return "No valid response from Gemini.";
            }
            return response.ToString();
            //var requestBody = new
            //{
            //    contents = new[]
            //  {
            //    new
            //    {
            //        role = "user",
            //        parts = new[]
            //        {
            //            new { text =  prompts  + userInput }
            //        }
            //    }
            //}
            //};


            //string jsonRequest = JsonSerializer.Serialize(requestBody);
            //HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await client.PostAsync(geminiUrl, content);
            //if (!response.IsSuccessStatusCode)
            //{
            //    return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            //}

            //string jsonResponse = await response.Content.ReadAsStringAsync();
            //using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            //JsonElement root = doc.RootElement;

            //if (root.TryGetProperty("candidates", out JsonElement candidates))
            //{
            //    return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
            //}

            //return "No valid response from Gemini.";
        }
    }
}

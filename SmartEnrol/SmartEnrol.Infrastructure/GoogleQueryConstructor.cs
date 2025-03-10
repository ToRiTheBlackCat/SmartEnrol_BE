using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;
using SmartEnrol.Repositories.Models;

namespace SmartEnrol.Infrastructure
{
    public class GoogleQueryConstructor
    {
        private readonly IConfiguration _configuration;

        SmartEnrolContext context;
        public GoogleQueryConstructor(IConfiguration configuration, SmartEnrolContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        public async Task<string> GenerateResponse(string input)
        {
            var outputQuery = await CallGoogleApi(input);
            return outputQuery;
        }

        private async Task<string> CallGoogleApi(string input)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var googleAi = new GoogleAi(apiKey);
            var aiModel = googleAi.CreateGenerativeModel(GoogleAIModels.Gemini2Flash);
            var config = new GenerationConfig()
            {
                MaxOutputTokens = 400,
                Temperature = 0.5,
            };
            var sqlScriptTxt = await ReadScriptFromFileAsync();
             sqlScriptTxt += "This is my DbContext class of my Sql Server Database. I will explain carefully for you to understand: University will contain the information of universities. UniMajor will contains the majors that universities teach; Major is all the majors that provided by the government in VietNam; The AdmissionMethodOfMajor will contains the admission methods that apply for that majors; AdmissionMethodOfUni will contain all the admission methods that universities have in that year; Area is the cities or provinces that students of universities are located; CharacteristicOfMajor are all the characteristics that suitable for that majors; Characteristic is the list of characteristic name;  CharacteristicOfStudent is when student login if they give their characteristic or hobby or what they like base on that to specify which characteristic are they; Others tables you can read to know. Caution that you cannot provide the Entities or fields of Entities that not exist in my DbContext and all the data in my Database is VietNamese but all fields are in English but with any fields that about name like major name, method name, university name,... just use contain not need exactly equal when compare. To be sure after give me an answer please scan that to compare with my DbContext if not correct please update that.";
            sqlScriptTxt += "You are an adviser. Here are your instruction: 1.You must condense your answer into about 800 words. 2. You will not use list, always in paragraph. 3. Be succinct. 4. You dont give me conclusion. 5. You must follow the provided docs if you see the query is related to it. 6. You dont give summary or conclusion again. 7. Your answer must be short to fit about 800 tokens. 8. DO not include the history of the previous answer into the new answer. 9. Use the history of previous queries to answer but not inlcude them in when answer. 10. Keep your answer short";
            var history = new List<Content>
            {
                new Content(sqlScriptTxt, Roles.User),
            };
            
            var chatSession = aiModel.StartChat(config: config, history: history);
            var response = await chatSession.GenerateContentAsync(sqlScriptTxt + input);
            if (response == null)
            {
                return "No valid response from Gemini.";
            }
            return response.ToString();
        }

        private async Task<string> ReadScriptFromFileAsync()
        {
            try
            {
                var models = context.Model;
                var entityNames = models.GetEntityTypes().Select(e => e.Name).ToList();
                if (entityNames.Count == 0)
                {
                    return "Error: No tables found!";
                }
                return string.Join("\n", entityNames);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}

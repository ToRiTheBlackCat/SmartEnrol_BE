using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
using SmartEnrol.Repositories.Models;

namespace SmartEnrol.Infrastructure
{
    public class QueryConstruction
    {
        private readonly IConfiguration _configuration;

        SmartEnrolContext context;
        public QueryConstruction(IConfiguration configuration, SmartEnrolContext context)
        {
            _configuration = configuration;
            this.context = context;

        }
        public async Task<string> GenerateQueryString(string inputQuery)
        {
            var outputQuery = await CallGeminiApi(inputQuery);

            return outputQuery;
        }

        private async Task<string> CallGeminiApi(string input)
        {
            var urlPath = _configuration["Gemini:ApiUrl"];
            var apiKey = _configuration["Gemini:ApiKey"];
            var geminiUrl = urlPath + apiKey;

            using HttpClient client = new HttpClient();

            var sqlScriptTxt = await ReadScriptFromFileAsync();
            sqlScriptTxt += "This is my DbContext class of my Sql Server Database. I will explain carefully for you to understand: University will contain the information of universities. UniMajor will contains the majors that universities teach; Major is all the majors that provided by the government in VietNam; The AdmissionMethodOfMajor will contains the admission methods that apply for that majors; AdmissionMethodOfUni will contain all the admission methods that universities have in that year; Area is the cities or provinces that students of universities are located; CharacteristicOfMajor are all the characteristics that suitable for that majors; Characteristic is the list of characteristic name;  CharacteristicOfStudent is when student login if they give their characteristic or hobby or what they like base on that to specify which characteristic are they; Others tables you can read to know. Caution that you cannot provide the Entities or fields of Entities that not exist in my DbContext and all the data in my Database is VietNamese but all fields are in English but with any fields that about name like major name, method name, university name,... just use contain not need exactly equal when compare. To be sure after give me an answer please scan that to compare with my DbContext if not correct please update that.";

            //sqlScriptTxt += "This is the script of my Sql Server Database. I will explain carefully for you to understand: Table University will contain the information of universities. Table UniMajor will contains the majors that university is teach; The table Major is all the majors of the government in VietNam; The AdmissionMethodOfMajor will contains the admission method that apply for that major; Table AdmissionMethodOfUni will contain all the admission method that university have in that year; Table area is the city or province that students of universities are located; Table CharacteristicOfMajor are all the characteristics that suitable for that major; Table Characteristic is the list of characteristic name; Table CharacteristicOfStudent is when student login if they give their characteristic or hobby or what they like base on that to specify which characteristic are they; Others tables you can read to know. Caution that you cannot provide the table or field that not exist in my script and all the data in my Database is VietNamese but all fiels are English and with any fields that about name like major name, method name, university name just use contain not need exactly equal when compare. To be sure after give me an answer please reread that to compare with my script if not correct please update that";

            var prompts = ".Your name is SmartEnrol and you were created by TriNHM, you are created to become a consultant agent that help give advice about admission method of universities in VietName but if the user chat anything that out of your scopes, you can answer it with your knowledge and if user continure to asking please provide a continuously answer for them until they start a new conversation. You need to read carefully the SQL Server script and informations that I provided before then you will receive the user input, then convert it into a SQL Server query that strictly related with my scripts for me to query data later. You will not to use all the tables just base on the question to find out the most main table then join with other just related to the user input, do not join any tables that not contains in the input. Just make the shortest and clearest answer; no need to explain your response. After create response please look throught again the DbContext to check if somethings that not correct please update it for me. You can only create one time response so please give me the most correct answer, you can deep thinking before answer take times to handle that. Here is the input of the user: ";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = sqlScriptTxt + prompts + input }
                    }
                }
            }
            };


            string jsonRequest = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(geminiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("candidates", out JsonElement candidates))
            {
                return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
            }

            return "No valid response from Gemini.";

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

                //string filePath = "..\\SmartEnrol.Infrastructure\\DBSmartEnrol.sql";

                //if (!File.Exists(filePath))
                //{
                //    return "Error: File not found!";
                //}

                //return await File.ReadAllTextAsync(filePath);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}

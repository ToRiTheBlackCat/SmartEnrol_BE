using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
using SmartEnrol.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        public async Task<string> GenerateQueryString(string inputQuery, string queryRouted)
        {
            var outputQuery = await CallGeminiApi(inputQuery, queryRouted);

            return outputQuery;
        }

        private async Task<string> CallGeminiApi(string input, string queryRouted)
        {
            var urlPath = _configuration["Gemini:ApiUrl"];
            var apiKey = _configuration["Gemini:ApiKey"];
            var geminiUrl = urlPath + apiKey;

            using HttpClient client = new HttpClient();

            var context = "";
            string Unipattern = @"\b(trường|major|ngành|phương thức|tuyển sinh|khoa|uni|university|chuyên ngành|methods|faculty|department)\b";
            string CharPttenrn = @"\b(tính cách|sở thích|habbit|hobby|phù hợp ngành|tính cách ngành)\b";

            if (queryRouted.Contains("special"))
            {
                //Switch case input
                if (Regex.IsMatch(input, Unipattern, RegexOptions.IgnoreCase))
                {
                    context += GenerateUniMajorAndAdmissionMethods() + "This is a reference for some universities that have some admission methods of that university and among that methods will be apply to some major throuhgt admission method of major and that will reference to the unimajor which contains all the major that university educate and you need to look throught Major to know what exactly that major name";
                }
                else if (Regex.IsMatch(input, CharPttenrn, RegexOptions.IgnoreCase))
                {
                    context += GenerateCharecteristicContext() + "This is a reference for which majors should you recommend based on given characteristic, recommend the top 5 major based on the user input(the major have to have the characteristics of the user)";
                }
            }
            else if (queryRouted.Contains("general"))
            {
                context += "this question is out of your scope because it not realated to your role as a admission consultant.";
            }
            var prompts = "Your mission is to receive user input and the revelant data about user input. If you receive the question out of your scope then just response None  -. If the question is in your scope then response with the data. You must answer all in VietNamese base on the data you received . And here is the user input: ";

            //var prompts = ".Your name is SmartEnrol, you are created to become a consultant agent that help give advice about admission method of universities in VietNam but if the user chat anything that out of your scopes or request for more information about universities, majors,..., you can answer it with your knowledge and if user continure to asking please provide a continuously answer for them until they start a new conversation.Help them answer their question just make the shortest and clearest answer. Here is the input of the user: ";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = context + prompts + input }
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


        private string GenerateUniMajorAndAdmissionMethods()
        {
            var universities = context.Universities.Include(x => x.AdmissionMethodOfUnis)
                                                   .ThenInclude(x => x.AdmissionMethodOfMajors)
                                                   .ThenInclude(x => x.UniMajor)
                                                   .ThenInclude(x => x.Major);

            var contextString = "";
            foreach (var uni in universities)
            {
                contextString += $"{uni.UniName} has some admission methods like: ";
                foreach (var adMethodOfUni in uni.AdmissionMethodOfUnis)
                {
                    contextString += $"{adMethodOfUni.MethodName} and this admission method has admission target ";
                    foreach (var adMethodOfMajor in adMethodOfUni.AdmissionMethodOfMajors)
                    {
                        contextString += $"{adMethodOfMajor.AdmissionTargets} and this admission method has the major score that student must have to apply to this school: {adMethodOfMajor.MajorScore}. And these requirements are apply for this major: ";
                        contextString += $"{adMethodOfMajor.UniMajor.Major.MajorName}.";

                    }
                }
            }

            return contextString;
        }
        private string GenerateCharecteristicContext()
        {

            var majors = context.Majors.Include(x => x.CharacteristicOfMajors).ThenInclude(x => x.Characteristic);

            var contextString = "";

            foreach (var major in majors)
            {
                contextString += $"{major.MajorName} includes characteristics like";
                foreach (var characterOfMajor in major.CharacteristicOfMajors)
                {
                    var character = characterOfMajor.Characteristic;
                    contextString += $" {character.CharacteristicName}, ";
                }

                if (major.CharacteristicOfMajors.Count > 0)
                {
                    contextString = contextString.Substring(0, contextString.Length - 2) + ". ";
                }
                else
                {
                    contextString += " None .";
                }
            }

            return contextString;
        }
    }
}






//var sqlScriptTxt = await ReadScriptFromFileAsync();
//sqlScriptTxt += "This is my DbContext class of my Sql Server Database. I will explain carefully for you to understand: University will contain the information of universities. UniMajor will contains the majors that universities teach; Major is all the majors that provided by the government in VietNam; The AdmissionMethodOfMajor will contains the admission methods that apply for that majors; AdmissionMethodOfUni will contain all the admission methods that universities have in that year; Area is the cities or provinces that students of universities are located; CharacteristicOfMajor are all the characteristics that suitable for that majors; Characteristic is the list of characteristic name;  CharacteristicOfStudent is when student login if they give their characteristic or hobby or what they like base on that to specify which characteristic are they; Others tables you can read to know. Caution that you cannot provide the Entities or fields of Entities that not exist in my DbContext and all the data in my Database is VietNamese but all fields are in English but with any fields that about name like major name, method name, university name,... just use contain not need exactly equal when compare. To be sure after give me an answer please scan that to compare with my DbContext if not correct please update that.";

//sqlScriptTxt += "This is the script of my Sql Server Database. I will explain carefully for you to understand: Table University will contain the information of universities. Table UniMajor will contains the majors that university is teach; The table Major is all the majors of the government in VietNam; The AdmissionMethodOfMajor will contains the admission method that apply for that major; Table AdmissionMethodOfUni will contain all the admission method that university have in that year; Table area is the city or province that students of universities are located; Table CharacteristicOfMajor are all the characteristics that suitable for that major; Table Characteristic is the list of characteristic name; Table CharacteristicOfStudent is when student login if they give their characteristic or hobby or what they like base on that to specify which characteristic are they; Others tables you can read to know. Caution that you cannot provide the table or field that not exist in my script and all the data in my Database is VietNamese but all fiels are English and with any fields that about name like major name, method name, university name just use contain not need exactly equal when compare. To be sure after give me an answer please reread that to compare with my script if not correct please update that";

//var prompts = ".Your name is SmartEnrol and you were created by TriNHM, you are created to become a consultant agent that help give advice about admission method of universities in VietName but if the user chat anything that out of your scopes, you can answer it with your knowledge and if user continure to asking please provide a continuously answer for them until they start a new conversation. You need to read carefully the SQL Server script and informations that I provided before then you will receive the user input, then convert it into a SQL Server query that strictly related with my scripts for me to query data later. You will not to use all the tables just base on the question to find out the most main table then join with other just related to the user input, do not join any tables that not contains in the input. Just make the shortest and clearest answer; no need to explain your response. After create response please look throught again the DbContext to check if somethings that not correct please update it for me. You can only create one time response so please give me the most correct answer, you can deep thinking before answer take times to handle that. Here is the input of the user: ";
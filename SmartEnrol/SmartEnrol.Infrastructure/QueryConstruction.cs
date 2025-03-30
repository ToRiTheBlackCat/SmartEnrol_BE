using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartEnrol.Repositories.Models;
using System.Text;
using System.Text.Json;
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
            string CharPattern = @"\b(tính cách|sở thích|habbit|hobby|phù hợp ngành|tính cách ngành)\b";
            string AreaPattern = @"\b(khu vực|area|địa điểm|vị trí|nơi|lân cận|thành phố|tỉnh thành|nơi này|locate|location|city|province)\b";

            if (queryRouted.Contains("special"))
            {
                //Switch case input
                if (Regex.IsMatch(input, Unipattern, RegexOptions.IgnoreCase))
                {
                    context += GenerateUniMajorAndAdmissionMethods() + "This is a reference for some universities that have some admission methods of that university and among that methods will be apply to some major throuhgt admission method of major and that will reference to the unimajor which contains all the major that university educate and you need to look throught Major to know what exactly that major name";
                }
                else if (Regex.IsMatch(input, CharPattern, RegexOptions.IgnoreCase))
                {
                    context += GenerateCharecteristicContext() + "This is a reference for which majors should you recommend based on given characteristic, recommend the top 5 major based on the user input(the major have to have the characteristics of the user)";
                }
                else if (Regex.IsMatch(input, AreaPattern, RegexOptions.IgnoreCase))
                {
                    context += GenerateSchoolInfoOfArea() + "This is a reference for which universities should you recommend based on given area name, rewrite again the question + the answer";
                }
            }
            else if (queryRouted.Contains("general"))
            {
                context += "this question is out of your scope because it not realated to your role as a admission consultant.";
            }
            var prompts = "Your mission is to receive user input and the revelant data about user input. If you receive the question out of your scope then just response None  -. If the question is in your scope then response with the data. You must answer all in VietNamese base on the data you received . And here is the user input: ";

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

        private string GenerateSchoolInfoOfArea()
        {
            var universities = context.Universities.Include(x => x.Area);

            var contextString = "";
            foreach (var uni in universities)
            {
                contextString += $"This University: {uni.UniName} is located in: {uni.Area.AreaName} ";
            }

            return contextString;
        }

        private string GenerateUniMajorAndAdmissionMethods()
        {
            var universities = context.Universities.Include(x => x.Area)
                                                   .Include(x => x.AdmissionMethodOfUnis)
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


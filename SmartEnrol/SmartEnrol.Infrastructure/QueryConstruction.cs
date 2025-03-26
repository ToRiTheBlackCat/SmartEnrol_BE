using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
using SmartEnrol.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Globalization;
using GenerativeAI.Types;

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


        private string GenerateSchoolInfoOfArea()
        {
            var universities = context.Universities.Include(x => x.Area);

            var contextString = "";
            foreach (var uni in universities)
            {
                contextString += $"This University: {uni.UniName} is located in: {uni.Area.AreaName} ";
            }

            return contextString;
            //string pattern = @"(?:\b(?:in|at|located in|near|around|within|in the area of|province of|city of)\s+(?:the\s+)?)([A-Z][a-zA-Z'-]+(?:\s+[A-Z][a-zA-Z'-]+)?)(?=\s+(?:City|Province|Region|District|area|\?|or|$))";
            //Match match = Regex.Match(userInput, pattern, RegexOptions.IgnoreCase);

            //if (!match.Success)
            //{
            //    return "Không xác định được khu vực từ câu hỏi.";
            //}

            //string areaName = match.Groups[1].Value.Trim();
            //string normalizedAreaName = NormalizeString(areaName);

            //var matchedUniversities = context.Universities
            //    .Include(x => x.Area)
            //    .AsEnumerable()
            //    .Where(x => NormalizeString(x.Area.AreaName).Contains(normalizedAreaName))
            //    .ToList();

            //if (!matchedUniversities.Any())
            //{
            //    return $"Không tìm thấy trường đại học nào ở khu vực {areaName}.";
            //}

            //return $"Ở khu vực {areaName}, có các trường đại học sau: {string.Join(", ", matchedUniversities.Select(u => u.UniName))}.";
        }
        //private string NormalizeString(string input)
        //{
        //    return string.Concat(input.Normalize(NormalizationForm.FormD)
        //                              .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
        //                 .ToLower();
        //}
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


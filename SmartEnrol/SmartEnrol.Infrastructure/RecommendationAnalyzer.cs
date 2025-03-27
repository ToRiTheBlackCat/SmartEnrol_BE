using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartEnrol.Infrastructure
{
    public class RecommendationAnalyzer
    {
        private readonly IConfiguration _configuration;

        private readonly SmartEnrolContext _context;

        public RecommendationAnalyzer(IConfiguration configuration, SmartEnrolContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<int>?> GetRecommendations(string input)
        {
            string response = await AnalyzeRecommendation(input);
            if (response.Contains("not"))
                return null;
            string[] list = response.Split('/');
            string[] listUniMajor;
            string[] listMajor;
            int uniMajorId;
            int? uniId = 0;
            int? majorId = 0;
            List<int> result = new List<int>();
            // Example response: "ĐẠI HỌC QUỐC GIA HÀ NỘI:Công nghệ thông tin;Vật lý học/Đại học Bách khoa Hà Nội:Kỹ thuật điện tử"
            // All the below comment will demonstrate ONE loop of the foreach clause
            foreach (string a in list) // list has "ĐẠI HỌC QUỐC GIA HÀ NỘI:Công nghệ thông tin;Vật lý học" and "Đại học Bách khoa Hà Nội:Kỹ thuật điện tử"
            {
                listUniMajor = a.Split(':'); // listUniMajor has "ĐẠI HỌC QUỐC GIA HÀ NỘI" and "Công nghệ thông tin;Vật lý học"
                listMajor = listUniMajor[1].Split(';'); // Split the latter one which is the major list
                foreach (string c in listMajor) // listMajor has "Công nghệ thông tin" and "Vật lý học"
                {
                    // Search id of "ĐẠI HỌC QUỐC GIA HÀ NỘI" based on matching name (provided Gemini responded with the exact data)
                    var university = _context.Universities.FirstOrDefault(x => x.UniName == listUniMajor[0]);
                    if (university != null)
                        uniId = university.UniId;

                    // Search id of "Công nghệ thông tin" based on matching name (provided Gemini responded with the exact data)
                    var major = _context.Majors.FirstOrDefault(x => x.MajorName == c);
                    if (major != null)
                        majorId = major.MajorId;

                    // Search id of the UniMajor entry with the matching uniId and majorId
                    var uniMajor = await _context.UniMajors.Where(c => c.UniId == uniId && c.MajorId == majorId).FirstOrDefaultAsync();
                    if (uniMajor != null)
                    {
                        // Nuff' said
                        uniMajorId = uniMajor.UniMajorId;
                        result.Add(uniMajorId);
                    }   
                }   
            }
            // Return a list of all the existing UniMajor entry id from the inputted response
            return result.Count>0 ? result:null;
        }

        private async Task<string> GetAllUniversityMajors()
        {      
            var items = await _context.UniMajors.Include(m => m.Major).Include(u => u.Uni).ToListAsync();

            string data = "";

            foreach(var obj in items)
            {
                data += $"The university {obj.Uni.UniName} ({obj.Uni.UniCode}) has major {obj.Major.MajorName}. ";
            }
            return data;
        }

        public async Task<string> AnalyzeRecommendation(string input)
        {
            var urlPath = _configuration["Gemini:ApiUrl"];
            var apiKey = _configuration["Gemini:ApiKey"];
            var geminiUrl = urlPath + apiKey;

            using HttpClient client = new HttpClient();

            string uniMajorData = await GetAllUniversityMajors();

            string aiContext = "You are an expert AI whose main purpose is natural language processing. " +
            "Your task is to process the input prompt and decide whether the prompt is a university and major recommendation that is also related to the provided documents or not by parsing the user's prompt and check whether there are related phrases, keywords and data related to the provided documents. " +
            "This task is performed to determine whether the input prompt - which is a response given by another AI answering a user's question about the topics of university enrollment and whether a major(s) is appropriate for them based on their current academics results alongside their personality - is a proper university and majors recommendation or not. " +
            "Follow these guidelines: " +
            "1. Analyze the input query for key concepts and intent. " +
            "2. Identify any ambiguous terms or phrases that could be clarified. " +
            "3. Consider common synonyms, related terms and alternative phrasings to more precisely determine the right context or the correct domain-specific knowledge. " +
            "4. Try not to deviate too far from the user's input's original intent. " +
            "5. Don't overanalyze the context or any terms or phrases of the original question. " +
            "6. If you don't recognize a word or acronym, try not to rewrite or paraphrase it if the meaning would be lost. " +
            "7. Keep all keywords or phrases that reference data within the provided document unchanged. " +
            "8. If the data in the provided document isn't in English, translate it into English as best as you can while still keeping to all the above guidelines." +
            "Your goal is to determine either the question is a recommendation of the related topics that is related to the provided documents or not. " +
            "If the prompt is confirmed to be a proper recommendation - " +
            "which must contains a university in the document provided, and majors that is available in that university, which must also be in the provided document" +
            " - format the response as follow (do not include the angle brackets, it is there to indicate the format of the input strings): " +
            //"<the original prompt unformatted>;{<recommended university 1>:<list of recommended major(s) within that university, each major separated by a semicolon>};{<recommended university 2>:<list of recommended major(s) within that university, each major separated by a semicolon>},and so on.... " +
            "<recommended university 1>:<list of recommended major(s) within that university, each major separated by a semicolon>/<recommended university 2>:<list of recommended major(s) within that university, each major separated by a semicolon>}/and so on.... " +
            "If not, then return a simple \"not\". " +
            $"Here is the provided documents: {uniMajorData}" +
            "Here is the input prompt: ";

            // Create a body using anonymous types (readonly, no need to define types explicitly, use obj initializer [new {}]
            var requestBodyAnalyze = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new {text = aiContext + input}
                        }
                    }
                }
            };

            // Prepare the requestBody for posting to the API
            string jsonRequest = JsonSerializer.Serialize(requestBodyAnalyze);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Post the request to the API and receive response
            HttpResponseMessage response = await client.PostAsync(geminiUrl, content);
            if(!response.IsSuccessStatusCode)
                return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Parsing the deserialized response into a Json object
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);

            // Start at root of json, then 
            JsonElement root = doc.RootElement;

            // Just read the example response body below and you'll understand
            // From root, find candidates property, output to a variable
            // From that variable, traverse in and get the text property
            if(root.TryGetProperty("candidates", out JsonElement candidates))
            {
                return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
            }
            return "No valid responses from Gemini.";
        }
    }
}
// Tbh just look up gemini 2's wiki page for this, I put this here for quick reference only

//Example request body (it's basically response's candidates)
/*
{
    "contents": [
        {
            "roles": string
            "parts": [
                {
                    "text": string
                }
            ];
        }
    ];
}
 */

//Example response body
/*
{
    "candidates": [
        {
            "content": {
                "parts": [
                    {
                        "text": "<AI response>\n"
                    }
                ],
            "role": "model"
            },
        "finishReason": "STOP",
        "avgLogprobs": -0.0019623951419540072
        }
    ],
    "usageMetadata": {
    "promptTokenCount": int,
    "candidatesTokenCount": int,
    "totalTokenCount": prompt + candidates,
    "promptTokensDetails": [
        {
            "modality": "TEXT",
            "tokenCount": int
        }
    ],
    "candidatesTokensDetails": [
        {
            "modality": "TEXT",
            "tokenCount": int
        }
    ]
},
"modelVersion": "gemini-2.0-flash"
}
 */

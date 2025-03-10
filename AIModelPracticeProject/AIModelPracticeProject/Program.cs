using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GenerativeAI;
using GenerativeAI.Types;
using Json.Schema;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static GenerativeAI.VertexAIModels;

namespace AIModelPracticeProject
{ 
    class Program
    {
        static async Task Main(string[] args)
        { 
            string apiKey = "replace this with your own key stupid https://aistudio.google.com/apikey";
            string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash";

            // Initialize HttpClient
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            string? originalQuestion;
            string? rewrittenQuestions = null;

            var googleAI = new GoogleAi(apiKey);
            var model = googleAI.CreateGenerativeModel("models/gemini-2.0-flash");

            var embeddingModel = googleAI.CreateEmbeddingModel("text-embedding-004");

            string contextRewrite = "You are an expert in query expansion and natural language processing. " +
                    "Your task is to generate an optimized search query based on the user's input." +
                    "Follow these guidelines: " +
                    "1. Analyze the input query for key concepts and intent." +
                    "2. Identify any ambiguous terms or phrases that could be clarified." +
                    "3. Consider common synonyms, related terms and alternative phrasings to improve the search." +
                    "4. If applicable, expand acronyms or abbreviations." +
                    "5. Incorporate any relevant context or domain-specific knowledge." +
                    "6. Ensure the expanded query maintains the original intent of the user's question." +
                    "7. Prioritize clarity and specificity in the rewritten query." +
                    "8. If the original query is already optimal, you may return it unchanged." +
                    "9. If you don't recognize a word or acronym, try not to rewrite or paraphrase it if the meaning would be lost." +
                    "Your goal is to produce a single, refined query that will return the best search results." +
                    "The rewritten query should be a natural language question or statement, not a list of keywords" +          
/*Step back*/       "Given a specific user question about one or more of these products, write a more generic question that needs to be answered in order to answer the specific question." +
/*Subquery*/        //"Return at least 3 and at most 5 options to choose from" +
/*Subquery*/        //"Format the rewritten queries as follow: Rewritten query options 1; Rewritten query options 2; Rewritten query options nth" +
                    "Do not include any explanation or generic response, only output the queries in the format above";

            string contextRouting = "You are an expert AI whose main purpose is natural language processing" +
                    "Your task is to process the user's input and decide whether the user's prompt is related to the provided documents or not" +
                    "by parsing the user's prompt and check whether there are related phrases and keywords with concepts related to the provided documents" +
                    "This task is used to route the user's prompt to the correct AI for querying and generating the correct response using the correct data" +
                    "Follow these guidelines: " +
                    "1. Analyze the input query for key concepts and intent." +
                    "2. Identify any ambiguous terms or phrases that could be clarified." +
                    "3. Consider common synonyms, related terms and alternative phrasings to more precisely determine the right context or the correct domain-specific knowledge." +
                    "4. Try not to deviate too far from the user's input's original intent." +
                    "5. Don't overanalyze the context or any terms or phrases of the original question." +
                    "Your goal is to determine either the question is related to the provided documents or the question is not related to the provided documents." +
                    "Return either [GENERAL] if you determined the context to not be related to the dataset" +
                    "or [SPECIALIZED] if you determined the context to be related to the dataset." +
                    "Provide between 1-2 explaination for why that prompt is routed to the path that you have chosen for it." +
                    "Format the response as follow: - Routing Path: \"[GENERAL] or [SPECIALIZED]" +
                    "\n- Explanation 1: \"Explanation Details\"" +
                    "\n- Explanation 2: \"Explanation Details\"" +
                    "Do not include any generic response, only output the queries in the format above.";

            /* string document = "This is the provided dataset in the format of [KEYWORD:DATA]: " +
                "Major:Information Technology;Major:Computer Engineering;Major:Physics;Major:Electronics Engineering;Major:Medicine;Major:Biotechnology;Major:Medical Laboratory Technology;Major:Literature;Major:History;Major:Geography;Major:Journalism;Major:Law;Major:English Language;Major:Business Administration;Major:International Economics;Major:Business Administration;Major:Finance and Banking;Major:Japanese Language;Major:Chinese Language;Major:Pharmacy" +
                "Area:Hồ Chí Minh;Area:Bà Rịa - Vũng Tàu;Area:Bắc Giang;Area:Bắc Kạn;Area:Bạc Liêu;Area:Bắc Ninh;Area:Bến Tre;Area:Bình Định;Area:Bình Dương;Area:Bình Phước;Area:Bình Thuận;Area:Cà Mau;Area:Cần Thơ;Area:Cao Bằng;Area:Đà Nẵng;Area:Đắk Lắk;Area:Đắk Nông;Area:Điện Biên;Area:Đồng Nai;Area:Đồng Tháp;Area:Gia Lai;Area:Hà Giang;Area:Hà Nam;Area:Hà Nội;Area:Hà Tĩnh;Area:Hải Dương;Area:Hải Phòng;Area:Hậu Giang;Area:Hòa Bình;Area:Hưng Yên;Area:Khánh Hòa;Area:Kiên Giang;Area:Kon Tum;Area:Lai Châu;Area:Lâm Đồng;Area:Lạng Sơn;Area:Lào Cai;Area:Long An;Area:Nam Định;Area:Nghệ An;Area:Ninh Bình;Area:Ninh Thuận;Area:Phú Thọ;Area:Phú Yên;Area:Quảng Bình;Area:Quảng Nam;Area:Quảng Ngãi;Area:Quảng Ninh;Area:Quảng Trị;Area:Sóc Trăng;Area:Sơn La;Area:Tây Ninh;Area:Thái Bình;Area:Thái Nguyên;Area:Thanh Hóa;Area:Thừa Thiên Huế;Area:Tiền Giang;Area:TP. Hồ Chí Minh;Area:Trà Vinh;Area:Tuyên Quang;Area:Vĩnh Long;Area:Vĩnh Phúc;Area:Yên Bái" +
                ""; */

            string documentRouting = "Majors: The specific fields of study that students choose to specialize in during their higher education, such as engineering, psychology, or business administration." +
                    "University: An institution of higher learning that offers undergraduate and graduate programs, conducts research, and grants academic degrees in various fields of study." +
                    "Characteristics: The inherent qualities or traits that define an individual’s personality, behavior, and abilities, which may influence their academic performance and learning style." +
                    "Admission Methods: The various procedures and criteria used by educational institutions to select and accept students, which may include entrance exams, interviews, academic records, and standardized test scores." +
                    "Area: A geographical division, such as a province, municipality, or city, that is often used to categorize schools, educational policies, and regional demographics in academic studies.";

            var config = new GenerationConfig
            {
                Temperature = 0.7f,
                MaxOutputTokens = 100
            };

            var historyRewrite = new List<Content>
            {
                new Content(contextRewrite, Roles.User),
            };
            
            var historyRouting = new List<Content>
            {
                new Content(documentRouting, Roles.User),
                new Content(contextRouting, Roles.User),
            };

            ChatSession chatRewrite = model.StartChat(config: config, history: historyRewrite);
            ChatSession chatRouting = model.StartChat(config: config, history: historyRouting);


            while (true)
            {
                // Collect question input from user
                Console.Write("Enter your question: ");
                originalQuestion = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(originalQuestion))
                {
                    Console.WriteLine("Question cannot be empty. Please try again.");
                    continue;
                }

                /*string context = "You are an AI assistant tasked with reformulating user queries to improve retrieval in a RAG system. " +
                    "Given the original query, rewrite it to be more specific, detailed, and likely to retrieve relevant information." +
                    //"Give around 3-5 options to choose from" +
                    "Format the rewritten queries as follow: Rewritten query options 1;Rewritten query options 2;Rewritten query options nth" +
                    "Do not include any explanation or generic response, only output the queries in the format above" +
                    "The original query is as follow: \"" + originalQuestion + "\".";*/
                
                try
                {
                    if (originalQuestion == "embed")
                    {
                        // Using the constructor:
                        //var content = new Content("This is a test embedding."); // Implicit User role

                        // Or with explicit role:
                        var contentWithRole = new Content("This is a test embedding.", Roles.User);

                        EmbedContentResponse responseEmbed = await embeddingModel.EmbedContentAsync(contentWithRole);

                        // Access the embedding
                        var embedding = responseEmbed.Embedding;
                        foreach (var o in embedding.Values)
                            Console.WriteLine(o + "\n");
                    }
                    else
                    {
                        var response = await chatRewrite.GenerateContentAsync(originalQuestion);
                        //Console.WriteLine("Google AI Response:");
                        Console.WriteLine(response.Text());
                        Console.WriteLine();

                        rewrittenQuestions = response.Text();
                        response = await chatRouting.GenerateContentAsync(rewrittenQuestions);
                        Console.WriteLine(response.Text());
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
               

                //Subquery prompting
                #region

                /*List<string> list = new List<String>(rewrittenQuestions.Split(';'));

                foreach(string item in list)
                {
                    Console.WriteLine(item.Trim());
                    Console.WriteLine();
                }*/

                #endregion
            }
        }
    }

	// Hugging Face model endpoint (DistilBERT for question answering)
	// string modelId = "distilbert-base-cased-distilled-squad"; // Fine-tuned for question answering
	// string apiUrl = $"https://api-inference.huggingface.co/models/{modelId}";

	// TODO List:
	// Context for: 
	//  Zero shot rewritting
	//  Step back prompting
	//  Few shot rewritting
	//  Subquery rewritting (Currently doing)
	//  HyDE (Hypothetical Document Embedding)
	// Integrate into main proj
	// Text embedding

	// Pick an options
	// Route query according to context (send to vector if according to docs, generic otherwise)
	// 

	//https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash

	//try catch below

	// Create the payload to send to the Hugging Face API
	//var payload = new
	//{
	//   inputs = new
	//   {
	//       question = question,
	//       context = context
	//   }
	//};

	/*var result = await chatSession.SendMessageAsync(message);
                    // Send the input to Hugging Face API
                    var response = await client.PostAsJsonAsync(apiUrl, payload);

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response content
                        var result = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);

                        // Access the answer
                        string answer = jsonResponse?.answer ?? "No answer found";
                        float score = jsonResponse?.score ?? "0.0000";
                        string scoreString = string.Format("{0:0.0000}", score);
                        string start = jsonResponse?.start ?? "No start";
                        string end = jsonResponse?.end ?? "No end";
                        Console.WriteLine($"AI Answer: {answer}, Score: {scoreString}, Start: {start}, End: {end}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                    }
                }*/
}

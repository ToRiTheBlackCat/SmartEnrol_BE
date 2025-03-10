using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GenerativeAI;
using GenerativeAI.Types;
using Json.Schema;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service;

namespace AIModelPracticeProject
{ 
    class SpecializedDocsAI
    {
        private readonly DocumentService _serv;

        public SpecializedDocsAI(DocumentService serv)
        {
            _serv = serv;
        }

        public async Task SpecializedAI()
        {
            string apiKey = "replace this with your own key stupid https://huggingface.co/settings/tokens";

            string modelId = "roberta-base-squad2"; // Fine-tuned for question answering
            string apiUrl = $"https://api-inference.huggingface.co/models/{modelId}";

            // Initialize HttpClient
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            string? originalQuestion;
            string? rewrittenQuestions = null;

            //string document = _serv.GetDocument();
            //NOTE: This is technically not how an actual RAG with a specialized dataset works, there's data embedding

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

                // Create the payload to send to the Hugging Face API
                var payload = new
                {
                   inputs = new
                   {
                       question = originalQuestion,
                       //context = document
                   }
                };

                try
                {
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
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
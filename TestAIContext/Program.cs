//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Http.Json;
//using Newtonsoft.Json;
//using GenerativeAI;
//namespace TestAIContext
//{
//    public class Program
//    {
//        static async Task Main(string[] args)
//        {
//            

//            //Hugging Face model endpoint(DistilBERT for question answering)
//            string modelId = "deepset/roberta-large-squad2"; // Fine-tuned for question answering
//            string apiUrl = $"https://api-inference.huggingface.co/models/{modelId}";

//            //Initialize HttpClient
//            using HttpClient client = new HttpClient();
//            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {api}");

//            //Predefined context
//            string context = "FPT University is the leading university that  teach technology in Viet Nam, located in Ho Chi Minh City. The weather like today is hot and windy. Your name is AI1, you are created by A. The university mainly teaches software-engineering, computer-science, international business.";

//            string? question;

//            while (true)
//            {
//                // Collect question input from user
//                Console.Write("Enter your question: ");
//                question = Console.ReadLine();

//                if (string.IsNullOrWhiteSpace(question))
//                {
//                    Console.WriteLine("Question cannot be empty. Please try again.");
//                    continue;
//                }

//                // Create the payload to send to the Hugging Face API
//                var payload = new
//                {
//                    inputs = new
//                    {
//                        question = question,
//                        context = context
//                    }
//                };

//                try
//                {
//                    // Send the input to Hugging Face API
//                    var response = await client.PostAsJsonAsync(apiUrl, payload);

//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Parse the response content
//                        var result = await response.Content.ReadAsStringAsync();

//                        // Deserialize the JSON response
//                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);

//                        // Access the answer
//                        string answer = jsonResponse?.answer ?? "No answer found";
//                        Console.WriteLine($"AI Answer: {answer}");
//                    }
//                    else
//                    {
//                        Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Exception: {ex.Message}");
//                }
//            }
//        }
//    }


//}


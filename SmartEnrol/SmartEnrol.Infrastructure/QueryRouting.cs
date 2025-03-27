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
	public class QueryRouting
	{
		private readonly IConfiguration _configuration;

		public QueryRouting(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> Route(string input)
		{
			var urlPath = _configuration["Gemini:ApiUrl"];
			var apiKey = _configuration["Gemini:ApiKey"];
			var geminiUrl = urlPath + apiKey;

			using HttpClient client = new HttpClient();

			string documentRouting = "Majors: The specific fields of study that students choose to specialize in during their higher education, such as engineering, psychology, or business administration." +
					"University: An institution of higher learning that offers undergraduate and graduate programs, conducts research, and grants academic degrees in various fields of study." +
					"Characteristics: The inherent qualities or traits that define an individual’s personality, behavior, and abilities, which may influence their academic performance and learning style." +
					"Admission Methods: The various procedures and criteria used by educational institutions to select and accept students, which may include entrance exams, interviews, academic records, and standardized test scores." +
					"Area: A geographical division, such as a province, municipality, or city, that is often used to categorize schools, educational policies, and regional demographics in academic studies.";

			string contextRouting = "You are an expert AI whose main purpose is natural language processing. " +
					"Your task is to process the user's input and decide whether the user's prompt is related to the provided documents or not " +
					"by parsing the user's prompt and check whether there are related phrases and keywords with concepts related to the provided documents. " +
					"This task is used to route the user's prompt to the correct AI for querying and generating the correct response using the correct data. " +
					"Follow these guidelines: " +
					"1. Analyze the input query for key concepts and intent. " +
					"2. Identify any ambiguous terms or phrases that could be clarified. " +
					"3. Consider common synonyms, related terms and alternative phrasings to more precisely determine the right context or the correct domain-specific knowledge. " +
					"4. Try not to deviate too far from the user's input's original intent. " +
					"5. Don't overanalyze the context or any terms or phrases of the original question. " +
					"Your goal is to determine either the question is related to the provided documents or the question is not related to the provided documents. " +
					"Format the response as follow: general or special. " +
					"Return either general if you determined the context to not be related to the dataset, " +
					"or special if you determined the context to be related to the dataset. " +
					//"Provide between 1-2 explanation for why that prompt is routed to the path that you have chosen for it." +
					"Do not include any generic response or explanation, only output the queries in the format above. " +
					$"Here is the document: {documentRouting}. " +
					"And here is the question: ";

			var requestBodyReroute = new
			{
				contents = new[]
				{
					new
					{
						role = "user",
						parts = new[]
						{
							new {text = contextRouting + input}
						}
					}
				}
			};

			string jsonRequest = JsonSerializer.Serialize(requestBodyReroute);
			HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(geminiUrl, content);
			if (!response.IsSuccessStatusCode)
				return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";

			string jsonResponse = await response.Content.ReadAsStringAsync();
			using JsonDocument doc1 = JsonDocument.Parse(jsonResponse);
			JsonElement root1 = doc1.RootElement;

			if (root1.TryGetProperty("candidates", out JsonElement candidates))
			{
				return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
			}
			return "No valid response from Gemini.";
		}
	}
}

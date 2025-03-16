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
	public class QueryRewrite
	{
		private readonly IConfiguration _configuration;

		public QueryRewrite(IConfiguration configuration, SmartEnrolContext context)
		{
			_configuration = configuration;
		}

		public async Task<string> ReWrite(string input)
		{
			var urlPath = _configuration["Gemini:ApiUrl"];
			var apiKey = _configuration["Gemini:ApiKey"];
			var geminiUrl = urlPath + apiKey;

			using HttpClient client = new HttpClient();

			string contextRewrite = "You are an expert in query expansion and natural language processing. " +
					"Your task is to generate an optimized search query based on the user's input. " +
					"Follow these guidelines: " +
					"1. Analyze the input query for key concepts and intent. " +
					"2. Identify any ambiguous terms or phrases that could be clarified. " +
					"3. Consider common synonyms, related terms and alternative phrasings to improve the search. " +
					"4. If applicable, expand acronyms or abbreviations. " +
					"5. Incorporate any relevant context or domain-specific knowledge. " +
					"6. Ensure the expanded query maintains the original intent of the user's question. " +
					"7. Prioritize clarity and specificity in the rewritten query. " +
					"8. If the original query is already optimal, you may return it unchanged. " +
					"9. If you don't recognize a word or acronym, try not to rewrite or paraphrase it if the meaning would be lost. " +
					"Your goal is to produce a single, refined query that will return the best search results. " +
					"The rewritten query should be a natural language question or statement, not a list of keywords. " +
/*Step back*/       "Given a specific user question about one or more of these products, write a more generic question that needs to be answered in order to answer the specific question. " +
/*Subquery*/        //"Return at least 3 and at most 5 options to choose from" +
/*Subquery*/        //"Format the rewritten queries as follow: Rewritten query options 1; Rewritten query options 2; Rewritten query options nth" +
					"Do not include any explanation or generic response, only output the queries in the format above. " +
					"If the question is in another language, translate it into English as best as you can while still keeping to all the above guidelines. " +
					"Here is the question: ";

			var requestBodyRewrite = new
			{
				contents = new[]
				{
					new
					{
						role = "user",
						parts = new[]
						{
							new {text = contextRewrite + input}
						}
					}
				}
			};

			string jsonRequest = JsonSerializer.Serialize(requestBodyRewrite);
			HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(geminiUrl, content);
			if(!response.IsSuccessStatusCode)
				return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";

			string jsonResponse = await response.Content.ReadAsStringAsync();
			using JsonDocument doc = JsonDocument.Parse(jsonResponse);
			JsonElement root = doc.RootElement;

			string rewrittenQuestion = null;


			if (root.TryGetProperty("candidates", out JsonElement candidates))
			{
				 return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
			}

			return "No valid response from Gemini.";
		}
	}
}

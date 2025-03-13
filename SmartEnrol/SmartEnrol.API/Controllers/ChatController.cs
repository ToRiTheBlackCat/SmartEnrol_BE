using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Infrastructure;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly PostRetrieval _service;
        private readonly QueryConstruction _query;

		private readonly QueryRewrite _qRewrite;
		private readonly QueryRouting _qRoute;

		public ChatController(PostRetrieval service, QueryConstruction query, QueryRewrite qRewrite, QueryRouting qRoute)
		{
			_service = service;
			_query = query;
			_qRewrite = qRewrite;
			_qRoute = qRoute;
		}

		[HttpPost("test-google-gemini-2")]
        public async Task<IActionResult> GetQuery(string inputQuery)
        {
            var data = await _query.GenerateQueryString(inputQuery);
            var response = await _service.GenerateResponse(inputQuery, data);
            return Ok(response);
        }

		[HttpPost("test-routing-rewrite")]
		public async Task<IActionResult> RewriteRerouteInput(string input)
		{
			var rewritten = await _qRewrite.CallGeminiApi(input);
			var result = await _qRoute.CallGeminiApi(rewritten);
			return Ok(new
			{
				routing = result
			});
		}
	}
}

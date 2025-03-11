using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartEnrol.Infrastructure;

namespace SmartEnrol.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AIController : Controller
	{
		private readonly QueryRewrite _queryWrite;
		private readonly QueryRouting _queryRoute;

		public AIController(QueryRewrite queryWrite, QueryRouting queryRoute)
		{
			_queryWrite = queryWrite;
			_queryRoute = queryRoute;
		}

		[HttpPost("test-routing-rewrite")]
		public async Task<IActionResult> RewriteRerouteInput(string input)
		{
			var rewritten = await _queryWrite.CallGeminiApi(input);
			var result = await _queryRoute.CallGeminiApi(rewritten);
			return Ok(new
			{
				routing = result
			}) ;
		}
	}
}

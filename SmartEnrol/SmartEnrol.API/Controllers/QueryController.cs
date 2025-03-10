using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Infrastructure;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : Controller
    {
        private readonly GoogleQueryConstructor _goggleQuery;

        public QueryController(GoogleQueryConstructor googleQuery)
        {
            _goggleQuery = googleQuery;
        }
        [HttpPost("test-google-gemini-2")]
        public async Task<IActionResult> GetQuery(string inputQuery)
        {
            var outputQuery = await _goggleQuery.GenerateResponse(inputQuery);
            return Ok(outputQuery);
        }
    }
}

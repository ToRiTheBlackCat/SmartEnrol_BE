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

        public ChatController(PostRetrieval service, QueryConstruction query)
        {
            _service = service;
            _query = query;
        }
        [HttpPost]
        public async Task<IActionResult> Chat(string inputQuery)
        {
            var data = await _query.GenerateQueryString(inputQuery);
            var response = await _service.GenerateResponse(inputQuery, data);
            return Ok(response);
        }
    }
}

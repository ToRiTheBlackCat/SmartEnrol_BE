using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Services.Services;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : Controller
    {
        private readonly IAreaService _serv;

        public AreaController(IAreaService serv)
        {
            _serv = serv;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAreaList()
        {
            var result = await _serv.GetAreasAsync();
            // Shorthand if statement
            return result == null ?
                NotFound(new
                {
                    Message = "No Area found!"
                })
                : Ok(result);
        }
    }
}

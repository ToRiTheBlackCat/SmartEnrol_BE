using Microsoft.AspNetCore.Mvc;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

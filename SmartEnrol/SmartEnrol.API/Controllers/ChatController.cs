using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Infrastructure;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {        
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat(string userInput)
        {
            string response = await _chatService.GenerateResponse(userInput);
            return Ok(response);
        }
    }
}

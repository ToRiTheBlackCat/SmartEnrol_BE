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

        /// <summary>
        /// Possible WIP - Don't use as normal chat
        /// </summary>
        [HttpPost("chatrec")]
        public async Task<IActionResult> ChatWithRecommend(string userInput, int accountId)
        {
            string response = await _chatService.GenerateResponseWithRecommend(userInput,accountId);
            return Ok(response);
        }

        /// <summary>
        /// Use this as normal chat
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Chat(string userInput)
        {
            string response = await _chatService.GenerateResponse(userInput);
            return Ok(response);
        }
        
        [HttpPost("test")]
        public async Task<IActionResult> TestRecommend(string userInput)
        {
            string response = await _chatService.Test(userInput);
            return Ok(response);
        }
    }
}

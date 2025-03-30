using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Infrastructure;
using SmartEnrol.Services.Constant;

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
        /// Chat api
        /// userInput - question from user
        /// sessionsID - current sessionID
        /// </summary>
        [HttpPost]
        [Authorize(Roles = ConstantEnum.Roles.STUDENT)]
        public async Task<IActionResult> Chat(string userInput, string? sessionsID)
        {
            sessionsID = _chatService.GetOrCreateSessionID();
            string response = await _chatService.GenerateResponse(userInput, sessionsID);
            return Ok(response);
        }

        /// <summary>
        /// Get the sessionId to start chat
        /// </summary>
        [HttpGet]
        public IActionResult StartSession()
        {
            string sessionID = _chatService.GetOrCreateSessionID();
            return Ok(new { sessionID });
        }

        /// <summary>
        /// Remove current sessionId 
        /// </summary>
        [HttpDelete]
        public IActionResult DeleteSession(string sessionID)
        {
            _chatService.DeleteSessionID(sessionID);
            return Ok();
        }
    }
}


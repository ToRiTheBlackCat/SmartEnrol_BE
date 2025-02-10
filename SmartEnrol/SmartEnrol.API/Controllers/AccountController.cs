using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Services.AccountSer;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Login account with email and password
        /// LoginModel
        /// Return isAuthenticated and token
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmailAndPassword([FromBody] LoginModel request)
        {
            //Check required fields
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest();

            //Authenticate account
            var (isAuthenticated, response) = await _accountService.Authenticate(request);

            return isAuthenticated 
                ? Ok(new
                    {
                        Token = response
                    }) 
                : BadRequest(new
                    {
                        Token = response
                    });
        }
    }
}

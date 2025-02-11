using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Services.AccountSer;
using SmartEnrol.Services.Helper;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly GoogleLogin _googleLogin;
        public AccountController(IAccountService accountService,
                                 GoogleLogin googleLogin)
        {
            _accountService = accountService;
            _googleLogin = googleLogin;
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
            var (isAuthenticated,accountId, response) = await _accountService.Authenticate(request);

            return isAuthenticated
                ? Ok(new
                {
                    AccountId = accountId,
                    Token = response
                })
                : BadRequest(new
                {
                    AccountId = accountId,
                    Token = response
                });
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest();

            var (isAuthenticated, accountId, response) = await _googleLogin.GoogleAuthorization(request);

            return isAuthenticated
                ? Ok(new
                {
                    AccountId = accountId,
                    Token = response
                })
                : BadRequest(new
                {
                    AccountId = accountId,
                    Token = response
                });
        }
    }
}

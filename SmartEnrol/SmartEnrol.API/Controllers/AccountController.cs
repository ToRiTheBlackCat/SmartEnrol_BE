using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Repositories.Models;
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

        [HttpPost("signup")]
        public async Task<IActionResult> AccountSignup([FromBody] AccountSignupModel account)
        {
            if(string.IsNullOrEmpty(account.AccountName) || string.IsNullOrEmpty(account.Email)
                || string.IsNullOrEmpty(account.Password) || string.IsNullOrEmpty(account.ConfirmPassword))
                return BadRequest();

            var (resultString, submittedData, returnData) = await _accountService.AccountSignup(account);
            return Ok(new
            {
                result = resultString,
                submitData = submittedData,
                returnData = returnData
            }
            );
        }

        [HttpPatch("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] StudentAccountProfileModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var check = await _accountService.CheckIfExist(model.AccountId);
            if (!check)
                return NotFound("Account not found.");           
            try
            {
                var updatedAccount = await _accountService.UpdateUserProfile(model);
                return updatedAccount != null
                    ? Ok(updatedAccount)
                    : NotFound("Account not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating profile: {ex.Message}");
            }
        }

    }
}

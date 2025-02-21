using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.AccountSer;
using SmartEnrol.Services.Constant;
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
        /// Get Account detail by id
        /// Return acocunt
        /// </summary>
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountDetailById(int accountId)
        {
            //Check if account exist
            var account = await _accountService.GetAccountById(accountId);
            return account == null
                ? NotFound(new
                {
                    Message = "Account not found with that id",
                    AccountName = string.Empty,
                    Email = string.Empty
                })
                : Ok(new
                {
                    Message = "Account found",
                    account.AccountName,
                    account.Email
                });
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Authenticate account
            var (isAuthenticated, accountId, response) = await _accountService.Authenticate(request);

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

        /// <summary>
        /// Signup Account
        /// SignupAccountModel
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> AccountSignup([FromBody] AccountSignupModel account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (account.Password != account.ConfirmPassword)
            {
                return Ok(new
                {
                    result = "Password doesn't match!",
                    submitData = account
                }
                );
            }

            var (resultString, submittedData) = await _accountService.AccountSignup(account);
            return Ok(new
            {
                result = resultString,
                submitData = submittedData
            }
            );
        }

        /// <summary>
        /// Update Account Profile
        /// StudentAccountProfileModel
        /// </summary>
        [HttpPatch("update-profile")]
        [Authorize(Roles = ConstantEnum.Roles.STUDENT)]
        public async Task<IActionResult> UpdateProfile([FromBody] StudentAccountProfileModel model)
        {
            //Check required fields
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Check if account exist
            var check = await _accountService.CheckIfExist(model.AccountId);
            if (!check)
                return NotFound("Account not found.");

            var updatedAccount = await _accountService.UpdateUserProfile(model);

            return updatedAccount != null
                ? Ok(updatedAccount)
                : BadRequest("Account not found.");
        }
    }
}

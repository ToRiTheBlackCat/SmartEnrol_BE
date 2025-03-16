using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnrol.Infrastructure;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.Constant;
using SmartEnrol.Services.Helper;
using SmartEnrol.Services.Services;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly GoogleLogin _googleLogin;
        private readonly QueryConstruction _query;
        public AccountController(IAccountService accountService,
                                 GoogleLogin googleLogin,
                                 QueryConstruction queryConstruction)
        {
            _accountService = accountService;
            _googleLogin = googleLogin;
            _query = queryConstruction;
        }

        /// <summary>
        /// Get all account
        /// Return List of all account
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAccountList(
            string? name,
            bool sortByNewestDate = false,
            int pageSize = 10,
            int pageNumber = 1
            )
        {
            var result = await _accountService.GetAccounts(name, sortByNewestDate, pageSize, pageNumber);
            return result.Accounts == null
                ? NotFound(new
                {
                    Message = "No Account found!"
                })
                : Ok(new
                {
                    TotalCounts = result.totalCounts,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Courses = result.Accounts
                });
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
                    message = "Account not found with that id",
                    account,
                })
                : Ok(new
                {
                    message = "Account found",
                    account
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

        /// <summary>
        /// get accounts by month
        /// </summary>
        [HttpGet("get-month/{month}")]
        public async Task<IActionResult> GetAccountsByMonth(int month)
        {
            var result = await _accountService.GetAccountsByMonth(month);
            return result == null
                ? NotFound(new
                {
                    Message = "No Account found!"
                })
                : Ok(result);
        }
    }
}

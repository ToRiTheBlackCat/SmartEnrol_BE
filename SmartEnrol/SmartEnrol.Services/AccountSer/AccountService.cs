using Microsoft.Extensions.Configuration;
using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.Helper;
using SmartEnrol.Services.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.AccountSer
{

    public class AccountService : IAccountService
    {
        private readonly AuthenticationJWT _authenticationJWT;
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;

        public AccountService(AuthenticationJWT authenticationJWT,
                               IConfiguration confiiguration,
                               UnitOfWork unitOfWork)
        {
            _authenticationJWT = authenticationJWT;
            _configuration = confiiguration;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string,AccountSignupModel?,Account?)> AccountSignup(AccountSignupModel account)
        {
            if(account.Password != account.ConfirmPassword)
            {
                return ("Password doesn't match!",account,null);
            }

            account.Password = EncodingHelper.ComputeSHA256Hash(account.Password + _configuration["SecretString"]);
            account.ConfirmPassword = EncodingHelper.ComputeSHA256Hash(account.Password + _configuration["SecretString"]);

            var existingUser = await _unitOfWork.AccountRepository
                .GetAccountByEmail(account.Email);

            if (existingUser != null)
            {
                if (existingUser.IsActive == true)
                    return ("This email has an active account!",account, null);
                return ("This account is deactivated!",account,null);
            }

            if (await _unitOfWork.AccountRepository.GetAccountByAccountName(account.AccountName) != null)
                return ("This username is taken!", account,null);

            Account newUser = new Account()
            {
                AccountName = account.AccountName,
                Email = account.Email,
                Password = account.Password,
                RoleId = 1
            };

            var result = await _unitOfWork.AccountRepository
                .AccountSignup(newUser);

            return ("Account created successfully!",account,result);
        }

        public async Task<(bool, string, string)> Authenticate(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return (false, "", "Invalid email or password! Try again");

            //Hash password after concat with secret string with SHA256 algorithm
            login.Password = EncodingHelper.ComputeSHA256Hash(login.Password + _configuration["SecretString"]);

            //Find existing user
            var foundUser = await _unitOfWork.AccountRepository
                .GetAccountByEmailAndPasswordAsync(login.Email, login.Password);

            //Check existing user
            if (foundUser == null)
                return (false, "", "Not found any account with that email or password!");

            //Check status of that foundUser
            if (foundUser.IsActive == false)
                return (false, "", "Account is not actived!");

            //Generate access token for user
            string accessToken = _authenticationJWT.GenerateJwtToken(foundUser);

            return (true, foundUser.AccountId.ToString().Trim(), accessToken);
        }


    }
}

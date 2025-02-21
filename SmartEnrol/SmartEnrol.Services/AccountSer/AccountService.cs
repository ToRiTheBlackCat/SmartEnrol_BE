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
using AutoMapper;
using SmartEnrol.Services.Constant;

namespace SmartEnrol.Services.AccountSer
{

    public class AccountService : IAccountService
    {
        private readonly AuthenticationJWT _authenticationJWT;
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(AuthenticationJWT authenticationJWT,
                               IConfiguration confiiguration,
                               UnitOfWork unitOfWork,
                               IMapper mapper)
        {
            _authenticationJWT = authenticationJWT;
            _configuration = confiiguration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(string, AccountSignupModel?)> AccountSignup(AccountSignupModel account)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Hash Pass for storage using sha256
                account.Password = EncodingHelper.ComputeSHA256Hash(account.Password + _configuration["SecretString"]);
                account.ConfirmPassword = EncodingHelper.ComputeSHA256Hash(account.Password + _configuration["SecretString"]);

                // Check existing account
                var existingUser = await _unitOfWork.AccountRepository
                    .GetAccountByEmail(account.Email);

                if (existingUser != null)
                {
                    if (existingUser.IsActive == true)
                        return ("This email has an active account!", account);
                    return ("This account is deactivated!", account);
                }

                // Check username availability
                var existingName = await _unitOfWork.AccountRepository.GetAccountByAccountName(account.AccountName);

                if (existingName != null)
                    return ("This username is taken!", account);

                // Create account if all check passes
                Account newUser = new Account()
                {
                    AccountName = account.AccountName,
                    Email = account.Email,
                    Password = account.Password,
                    RoleId = (int)ConstantEnum.RoleID.STUDENT,
                    AreaId = 1,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                var result = await _unitOfWork.AccountRepository
                    .AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                // If database transaction fails
                if (result == 0)
                    throw new Exception("Error creating account!");

                await _unitOfWork.CommitTransactionAsync();

                var user = await _unitOfWork.AccountRepository.GetAccountByEmail(account.Email);

                return ("Account created successfully!", account);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
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

        public async Task<bool> CheckIfExist(int accountId)
        {
            var acc = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (acc == null)
                return false;
            return true;
        }

        public async Task<Account?> UpdateUserProfile(StudentAccountProfileModel acc)
        {
            if (acc == null)
                return null;
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var foundUser = await _unitOfWork.AccountRepository.GetByIdAsync(acc.AccountId);
                if (foundUser == null)
                    return null;
                Account account = _mapper.Map<StudentAccountProfileModel, Account>(acc);
                account.Password = foundUser.Password;
                account.IsActive = foundUser.IsActive;
                account.RoleId = foundUser.RoleId;
                account.CreatedDate = foundUser.CreatedDate;
                var up = await _unitOfWork.AccountRepository.UpdateAsync(account);
                await _unitOfWork.SaveChangesAsync();
                if (up == null)
                    throw new Exception("Update user profile failed!");
                await _unitOfWork.CommitTransactionAsync();
                return up;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
        public async Task<Account?> GetAccountById(int accountId)
        {
            var isExisted = await CheckIfExist(accountId);
            if (!isExisted)
                return null;

            var foundAccount = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            return foundAccount;

        }
    }
}

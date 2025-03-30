using Microsoft.Extensions.Configuration;
using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.Constant;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.Services.Helper
{
    public class GoogleLogin
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationJWT _authenticationJWT;


        public GoogleLogin(UnitOfWork unitOfWork,
                          IConfiguration confiiguration,
                          AuthenticationJWT authenticationJWT)
        {
            _unitOfWork = unitOfWork;
            _configuration = confiiguration;
            _authenticationJWT = authenticationJWT;
        }
        public async Task<(bool, string?, string)> GoogleAuthorization(GoogleLoginModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                    return (false, "", "Invalid email! Try again");

                await _unitOfWork.BeginTransactionAsync();
                string accessToken = "";

                var existedUser = await _unitOfWork.AccountRepository.GetAccountByEmail(request.Email);
                if (existedUser == null)
                {
                    var pass = EncodingHelper.ComputeSHA256Hash(request.Email + _configuration["SecretString"]);

                    var newAccount = new Account
                    {
                        Email = request.Email,
                        Password = pass,
                        RoleId = (int)ConstantEnum.RoleID.STUDENT,
                        AccountName = request.Name,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };
                    await _unitOfWork.AccountRepository.AddAsync(newAccount);
                    await _unitOfWork.SaveChangesAsync();


                    var foundUser = await _unitOfWork.AccountRepository.GetAccountByEmailAndPasswordAsync(request.Email, pass);
                    accessToken = _authenticationJWT.GenerateJwtToken(foundUser);

                    return (true, foundUser?.AccountId.ToString().Trim(), accessToken);
                }

                accessToken = _authenticationJWT.GenerateJwtToken(existedUser);
                await _unitOfWork.CommitTransactionAsync();

                return (true, existedUser.AccountId.ToString().Trim(), accessToken);

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return (false, "", ex.Message);
            }
        }
    }
}

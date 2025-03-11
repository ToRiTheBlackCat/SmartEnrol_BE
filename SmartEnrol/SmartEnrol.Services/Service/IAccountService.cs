using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.Services.Services
{
    public interface IAccountService
    {
        Task<(string, AccountSignupModel?)> AccountSignup(AccountSignupModel account);
        Task<(bool, string, string)> Authenticate(LoginModel login);
        Task<Account?> UpdateUserProfile(StudentAccountProfileModel acc);
        Task<bool> CheckIfExist(int accountId);
        Task<Account?> GetAccountById(int accountId);
        Task<IEnumerable<Account?>> GetAccounts();
    }
}

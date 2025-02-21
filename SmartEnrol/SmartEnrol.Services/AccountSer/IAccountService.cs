using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.AccountSer
{
    public interface IAccountService
    {
        Task<(string, AccountSignupModel?)> AccountSignup(AccountSignupModel account);
        Task<(bool, string, string)> Authenticate(LoginModel login);
        Task<Account?> UpdateUserProfile(StudentAccountProfileModel acc);
        Task<bool> CheckIfExist(int accountId);
        Task<Account?> GetAccountById(int accountId);
    }
}

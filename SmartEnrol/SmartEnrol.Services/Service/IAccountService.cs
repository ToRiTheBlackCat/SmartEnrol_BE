﻿using SmartEnrol.Repositories.Models;
using SmartEnrol.Services.ViewModels.Student;

namespace SmartEnrol.Services.Services
{
    public interface IAccountService
    {
        Task<(string, AccountSignupModel?)> AccountSignup(AccountSignupModel account);
        Task<(bool, string, string)> Authenticate(LoginModel login);
        Task<StudentAccountProfileModel> UpdateUserProfile(StudentAccountProfileModel acc);
        Task<bool> CheckIfExist(int accountId);
        Task<StudentAccountProfileModel?> GetAccountById(int accountId);
        Task<IEnumerable<Account?>> GetAccounts();
        Task<List<Account>> GetAccountsByMonth(int month);
    }
}

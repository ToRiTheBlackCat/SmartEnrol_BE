using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;

namespace SmartEnrol.Repositories.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetAccountByEmailAndPasswordAsync(string email, string password);
        Task<Account?> GetAccountByEmail(string email);
        Task<Account?> GetAccountByAccountName(string accountName);
        Task<List<Account>> GetAccountsByMonth(int month);
        Task<(IEnumerable<Account?> Accounts, int totalCounts)> GetAllAccountsAsync(int pageSize, int pageNumber);
        Task<(IEnumerable<Account?> Accounts, int totalCounts)> GetAccountsByNameAsync(string? name, int pageSize, int pageNumber);
    }
}

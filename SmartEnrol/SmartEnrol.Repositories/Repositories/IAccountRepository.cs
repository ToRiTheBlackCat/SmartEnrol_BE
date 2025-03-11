using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;

namespace SmartEnrol.Repositories.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetAccountByEmailAndPasswordAsync(string email, string password);
        Task<Account?> GetAccountByEmail(string email);
        Task<Account?> GetAccountByAccountName(string accountName);
    }
}

using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetAccountByEmailAndPasswordAsync(string email, string password);
        Task<Account?> GetAccountByEmail(string email);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SmartEnrol.Repositories.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(SmartEnrolContext context) : base(context) { }


        public async Task<Account?> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            var foundAccount = await _dbSet
                  .FirstOrDefaultAsync(s => s.Email == email &&
                                              s.Password == password);
            if (foundAccount == null)
            {
                return null;
            }
            var includedAccount = await GetAccountByIdWithIncludeAsync(foundAccount);

            return includedAccount;
        }
        private async Task<Account?> GetAccountByIdWithIncludeAsync(Account account)
        {
            return await GetByIdWithIncludeAsync(account.AccountId, "AccountId", x => x.WishListItems,
                                                                                 x => x.Role);

        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Email == email);
        }



    }
}

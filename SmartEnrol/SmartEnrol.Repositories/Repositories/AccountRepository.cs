﻿using Microsoft.EntityFrameworkCore;
using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;

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
            return await GetByIdWithIncludeAsync(account.AccountId, "AccountId", x => x.Role,
                                                                                 x => x.Area);
        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            var foundAccount = await _dbSet.FirstOrDefaultAsync(s => s.Email == email);
            if (foundAccount == null)
            {
                return null;
            }
            var includedAccount = await GetAccountByIdWithIncludeAsync(foundAccount);

            return includedAccount;
        }

        public async Task<Account?> GetAccountByAccountName(string accountName)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.AccountName == accountName);
        }

        public async Task<List<Account>> GetAccountsByMonth(int month)
        {
            return await _dbSet.Where(s => s.CreatedDate.Month == month).ToListAsync();
        }

        public async Task<(IEnumerable<Account?> Accounts, int totalCounts)> GetAllAccountsAsync( 
                                                                            int pageSize = 10, 
                                                                            int pageNumber = 1)
        {
            var totalCount = await _context.Set<Account>().CountAsync();
            var accounts = await _context.Set<Account>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (accounts, totalCount);
        }

        public async Task<(IEnumerable<Account?> Accounts, int totalCounts)> GetAccountsByNameAsync(
                                                                            string? name,
                                                                            int pageSize,
                                                                            int pageNumber)
        {
            var totalCount = await _context.Set<Account>().CountAsync();
            var accounts = await _context.Set<Account>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(a => a.AccountName.Contains(name))
                .ToListAsync();

            return (accounts, totalCount);
        }
    }
}

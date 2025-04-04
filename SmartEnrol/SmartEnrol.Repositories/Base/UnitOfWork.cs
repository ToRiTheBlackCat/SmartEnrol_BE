﻿using Microsoft.EntityFrameworkCore.Storage;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Repositories.Repositories;

namespace SmartEnrol.Repositories.Base
{
    public class UnitOfWork
    {
        private readonly SmartEnrolContext _context;
        private IDbContextTransaction? _transaction;

        public IAccountRepository AccountRepository { get; }
        public IAreaRepository AreaRepository { get; }

        public UnitOfWork(SmartEnrolContext context, IAccountRepository accountRepository, IAreaRepository areaRepository)
        {
            _context = context;
            AccountRepository = accountRepository;
            AreaRepository = areaRepository;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

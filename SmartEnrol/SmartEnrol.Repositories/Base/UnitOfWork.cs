using Microsoft.EntityFrameworkCore.Storage;
using SmartEnrol.Repositories.Models;
using SmartEnrol.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Base
{
    public class UnitOfWork
    {
        private readonly SmartEnrolContext _context;
        private IDbContextTransaction? _transaction;

        public IAccountRepository AccountRepository { get; }
        public IAreaRepository AreaRepository { get; }
        public IRecommendationRepository RecommendationRepository { get; }
        public IRecommendationDetailRepository RecommendationDetailRepository { get; }

        public UnitOfWork(
            SmartEnrolContext context,
            IAccountRepository accountRepository,
            IAreaRepository areaRepository,
            IRecommendationRepository recommendationRepository,
            IRecommendationDetailRepository recommendationDetailRepository)
        {
            _context = context;
            AccountRepository = accountRepository;
            AreaRepository = areaRepository;
            RecommendationRepository = recommendationRepository;
            RecommendationDetailRepository = recommendationDetailRepository;
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

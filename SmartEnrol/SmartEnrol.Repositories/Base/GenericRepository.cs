using Microsoft.EntityFrameworkCore;
using SmartEnrol.Repositories.Models;
using System.Linq.Expressions;

namespace SmartEnrol.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SmartEnrolContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SmartEnrolContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Get all with generic include of entity
        /// EXAMPLE HOW TO USE
        ///  var products = (await _unitOfWork.ProductRepository.GetAllWithIncludeAsync(p => p.Category)).AsQueryable();
        /// </summary>
        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public T? GetById(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task<T?> GetByIdAsync(int TId)
        {
            var entity = await _dbSet.FindAsync(TId);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        /// <summary>
        /// Get detail with id and with generic include of entity
        /// EXAMPLE HOW TO USE
        /// var foundCart = await _unitOfWork.CartRepository.GetByIdWithIncludeAsync(request.CartId, "CartId", cart => cart.ShoppingCartItems);
        /// </summary>
        public async Task<T?> GetByIdWithIncludeAsync(int TId, string typeId, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, typeId) == TId);

        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public async Task<int> AddAsync(T item)
        {
            _dbSet.Add(item);
            return await _context.SaveChangesAsync();
        }

        public void Update(T item)
        {
            var tracker = _context.Attach(item);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
        public bool Delete(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync(T? item)
        {
            if (item == null)
                return false;

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> UpdateAsync(T item)
        {
            var tracker = _context.Attach(item);
            tracker.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}

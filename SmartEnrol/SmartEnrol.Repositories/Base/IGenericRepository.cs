using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);
        T? GetById(int id);
        Task<T?> GetByIdAsync(int TId);
        Task<T?> GetByIdWithIncludeAsync(int TId, string typeId, params Expression<Func<T, object>>[] includeProperties);
        void Add(T item);
        Task<int> AddAsync(T item);
        void Update(T item);
        Task<int> UpdatAeAsync(T item);
        bool Delete(T item);
        Task<bool> DeleteAsync(T? item);

    }
}

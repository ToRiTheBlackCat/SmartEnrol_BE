using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface IUniversitoryRepository
    {
        Task<IEnumerable<University>> GetUniversities();
    }
    public class UniversitoryRepository : IUniversitoryRepository
    {
        private readonly SmartEnrolDBContext _context;
        public UniversitoryRepository( SmartEnrolDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<University>> GetUniversities()
        {
            return await Task.FromResult(_context.Universities.ToList());
        }
    }
}

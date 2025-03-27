using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Repositories
{
    public interface IRecommendationRepository : IGenericRepository<Recommendation>
    {
        Task<(int,int)> CreateAsync(int accId);
    }
}

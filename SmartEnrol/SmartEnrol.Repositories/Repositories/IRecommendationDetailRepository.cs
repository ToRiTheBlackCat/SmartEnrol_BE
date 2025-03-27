using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Repositories
{
    public interface IRecommendationDetailRepository : IGenericRepository<RecommendationDetail>
    {
        Task<int> CreateAsync(int uniMajId, int reccId, string recommendation);
    }
}

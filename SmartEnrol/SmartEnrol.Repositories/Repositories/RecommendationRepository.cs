using Microsoft.EntityFrameworkCore;
using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Repositories
{
    public class RecommendationRepository : GenericRepository<Recommendation>, IRecommendationRepository
    {
        private readonly IRecommendationDetailRepository _recDetailRepo;

        public RecommendationRepository(SmartEnrolContext context) : base(context)
        {
        }

        public async Task<(int,int)> CreateAsync(int accId)
        {
            Recommendation recc = new Recommendation();
            recc.AccountId = accId;
            recc.CreatedDate = DateTime.Now;
            var reccId = _context.Recommendations.Count() + 1;
            recc.RecommendationId = reccId;
            _context.Recommendations.Add(recc);
            var result = await _context.SaveChangesAsync();
            return (result, reccId);
        }
    }
}

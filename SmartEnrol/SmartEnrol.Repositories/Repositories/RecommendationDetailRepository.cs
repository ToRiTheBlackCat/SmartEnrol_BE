using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Repositories.Repositories
{
    public class RecommendationDetailRepository : GenericRepository<RecommendationDetail>, IRecommendationDetailRepository
    {
        public RecommendationDetailRepository(SmartEnrolContext context) : base(context)
        {
        }

        public async Task<int> CreateAsync(int uniMajId, int reccId, string recommendation)
        {
            RecommendationDetail reccDet = new RecommendationDetail();
            reccDet.RecommendationId = reccId;
            reccDet.UniMajorId = uniMajId;
            reccDet.Recommendation = recommendation;
            var reccDetId = _context.RecommendationDetails.Count() + 1;
            reccDet.RecommendationDetailId = reccDetId;
            _context.RecommendationDetails.Add(reccDet);
            return await _context.SaveChangesAsync();
        }
    }
}

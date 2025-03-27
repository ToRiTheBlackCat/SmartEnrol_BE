using SmartEnrol.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Service
{
    public class RecommendationService : IRecommendationService
    {
        private readonly UnitOfWork _unitOfWork;

        public RecommendationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(int uniMajId, int accId, string recommendation)
        {
            (int operation, int reccId) result = await _unitOfWork.RecommendationRepository.CreateAsync(accId);
            if(result.operation > 0)
            {
                return await _unitOfWork.RecommendationDetailRepository.CreateAsync(uniMajId, result.reccId, recommendation);
            }
            return -1;
        }
    }
}

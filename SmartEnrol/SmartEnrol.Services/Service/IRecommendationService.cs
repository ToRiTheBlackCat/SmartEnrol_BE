using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Service
{
    public interface IRecommendationService
    {
        Task<int> Create(int uniMajId, int accId, string recommendation);
    }
}

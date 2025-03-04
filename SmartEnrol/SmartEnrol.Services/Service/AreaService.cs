using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Services
{
    public class AreaService : IAreaService
    {
        private readonly UnitOfWork _unitOfWork;

        public AreaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Area>> GetAreasAsync()
        {
            return await _unitOfWork.AreaRepository.GetAllAsync();
        }
    }
}

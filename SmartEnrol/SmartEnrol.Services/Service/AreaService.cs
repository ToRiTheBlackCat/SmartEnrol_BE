using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;

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

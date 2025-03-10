using SmartEnrol.Repositories.Models;

namespace SmartEnrol.Services.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAreasAsync();
    }
}

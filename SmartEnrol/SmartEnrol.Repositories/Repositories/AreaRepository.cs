using SmartEnrol.Repositories.Base;
using SmartEnrol.Repositories.Models;

namespace SmartEnrol.Repositories.Repositories
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        public AreaRepository(SmartEnrolContext context) : base(context) { }
    }
}

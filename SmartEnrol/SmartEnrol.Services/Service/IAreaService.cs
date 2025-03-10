using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAreasAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IRegionalCenterRepo : IGenericRepo<RegionalCenter>
    {
        Task<RegionalCenter> GetRegionalCenter(long id);
        Task<IEnumerable<RegionalCenter>> GetRegionalCenters();
    }
}

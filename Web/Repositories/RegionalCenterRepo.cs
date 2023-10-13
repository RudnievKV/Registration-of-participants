using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories
{
    public class RegionalCenterRepo : GenericRepo<RegionalCenter>, IRegionalCenterRepo
    {
        public RegionalCenterRepo(MyDBContext context) : base(context)
        {
        }

        public async Task<RegionalCenter> GetRegionalCenter(long id)
        {
            return await _context.RegionalCenters
                .Where(s => s.RegionalCenter_ID == id)
                .Include(s => s.Users)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<RegionalCenter>> GetRegionalCenters()
        {
            return await _context.RegionalCenters
                .Include(s => s.Users)
                .ToListAsync();
        }
    }
}
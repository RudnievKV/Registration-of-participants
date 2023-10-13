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
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(MyDBContext context): base(context) 
        {

        }

        public async Task<User> GetUser(long id)
        {
            return await _context.Users
                .Where(s => s.User_ID == id)
                .Include(s => s.RegionalCenter)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Include(s => s.RegionalCenter)
                .ToListAsync();
        }
    }
}
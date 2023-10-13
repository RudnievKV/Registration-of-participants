using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User> GetUser(long id);
        Task<IEnumerable<User>> GetUsers();
    }
}

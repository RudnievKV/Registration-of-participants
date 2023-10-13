using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Repositories.Interfaces;

namespace Web.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo UserRepo { get; }
        IRegionalCenterRepo RegionalCenterRepo { get; }
        Task<int> SaveChangesAsync();

    }
}

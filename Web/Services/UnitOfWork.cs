using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Web.Common;
using Web.Dtos;
using Web.Models;
using Web.Repositories;
using Web.Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _context;
        private bool disposedValue;

        public UnitOfWork(MyDBContext context)
        {
            _context = context;
            UserRepo = new UserRepo(context);
            RegionalCenterRepo = new RegionalCenterRepo(context);

        }

        public IUserRepo UserRepo { get; private set; }
        public IRegionalCenterRepo RegionalCenterRepo { get; private set; }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Repositories;
using TransportLibrary.Authentication.Repositories.Interfaces;
using TransportLibrary.Settings;
using TransportLibrary.Shared.Repositories.Interfaces;

namespace TransportLibrary.Shared.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportDbContext _context;
        private IApplicationUserRepository _applicationUsers;
        private IApplicationRoleRepository _applicationRoles;

        public UnitOfWork(TransportDbContext context)
        {
            _context = context;
        }

        public IApplicationUserRepository ApplicationUsers => _applicationUsers ??= new ApplicationUserRepository(_context);

        public IApplicationRoleRepository ApplicationRoles => _applicationRoles ??= new ApplicationRoleRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

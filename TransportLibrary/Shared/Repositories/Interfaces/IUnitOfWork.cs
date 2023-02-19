using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Repositories.Interfaces;

namespace TransportLibrary.Shared.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public void Save();

        public IApplicationUserRepository ApplicationUsers { get; }

        public IApplicationRoleRepository ApplicationRoles { get; }

    }
}

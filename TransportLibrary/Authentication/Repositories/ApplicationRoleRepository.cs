using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Repositories.Interfaces;
using TransportLibrary.Settings;
using TransportLibrary.Shared.Repositories;

namespace TransportLibrary.Authentication.Repositories
{
    public class ApplicationRoleRepository : Repository<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

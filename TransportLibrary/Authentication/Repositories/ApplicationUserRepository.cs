using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Model;
using TransportLibrary.Authentication.Repositories.Interfaces;
using TransportLibrary.Settings;
using TransportLibrary.Shared.Repositories;

namespace TransportLibrary.Authentication.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

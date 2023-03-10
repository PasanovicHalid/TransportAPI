using Application.Common.Interfaces.Persistance.Employees;
using Infrastructure.Common.Persistance;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Repositories.Employees
{
    public class UserRepository : Repository<IdentityUser>, IUserRepository
    {
        public UserRepository(TransportDbContext db) : base(db)
        {

        }
    }
}

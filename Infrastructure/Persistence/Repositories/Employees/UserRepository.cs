using Application.Common.Interfaces.Persistence.Employees;
using Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Repositories.Employees
{
    public class UserRepository : Repository<IdentityUser>, IUserRepository
    {
        public UserRepository(TransportDbContext db) : base(db)
        {

        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces.Persistence.Employees
{
    public interface IUserRepository : IRepository<IdentityUser>
    {

    }
}

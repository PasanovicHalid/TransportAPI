using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces.Persistance.Employees
{
    public interface IUserRepository : IRepository<IdentityUser>
    {

    }
}

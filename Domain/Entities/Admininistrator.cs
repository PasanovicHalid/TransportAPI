using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Admininistrator : Employee
    {
        public Admininistrator(IdentityUser user,
                               string role,
                               string firstName,
                               string? middleName,
                               string lastName,
                               double salary,
                               Address address,
                               ulong companyId) : base(user, role, firstName, middleName, lastName, salary, address, companyId)
        {
        }

        protected Admininistrator()
        {
        }
    }
}

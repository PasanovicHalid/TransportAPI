using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

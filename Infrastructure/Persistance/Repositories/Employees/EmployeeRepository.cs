using Application.Common.Interfaces.Persistance.Employees;
using Domain.Employees;
using Infrastructure.Common.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Employees
{
    public class EmployeeRepository : EntityRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

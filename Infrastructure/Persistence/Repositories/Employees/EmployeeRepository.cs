using Application.Common.Interfaces.Persistence.Employees;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Employees
{
    public class EmployeeRepository : EntityRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

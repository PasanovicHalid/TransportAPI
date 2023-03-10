using Application.Common.Interfaces.Persistance.Employees;
using Domain.Employees;
using Infrastructure.Common.Persistance;

namespace Infrastructure.Persistance.Repositories.Employees
{
    public class EmployeeRepository : EntityRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

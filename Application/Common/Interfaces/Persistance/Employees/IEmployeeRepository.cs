using Domain.Employees;

namespace Application.Common.Interfaces.Persistance.Employees
{
    public interface IEmployeeRepository : IEntityRepository<Employee>
    {
    }
}

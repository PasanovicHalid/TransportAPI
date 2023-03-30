using Application.Common.Interfaces.Persistence.Employees;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Employees
{
    public class DriverRepository : EntityRepository<Driver>, IDriverRepository
    {
        public DriverRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

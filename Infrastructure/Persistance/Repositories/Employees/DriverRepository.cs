using Application.Common.Interfaces.Persistance.Employees;
using Domain.Drivers;
using Infrastructure.Common.Persistance;

namespace Infrastructure.Persistance.Repositories.Employees
{
    public class DriverRepository : EntityRepository<Driver>, IDriverRepository
    {
        public DriverRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

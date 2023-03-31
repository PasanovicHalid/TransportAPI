using Application.Common.Interfaces.Persistence.Employees;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Employees
{
    public class AdministratorRepository : EntityRepository<Admininistrator>, IAdministratorRepository
    {
        public AdministratorRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

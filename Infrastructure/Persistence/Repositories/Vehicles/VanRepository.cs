using Application.Common.Interfaces.Persistence.Vehicles;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Vehicles
{
    public class VanRepository : EntityRepository<Van>, IVanRepository
    {
        public VanRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

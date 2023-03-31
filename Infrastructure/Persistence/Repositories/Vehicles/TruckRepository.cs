using Application.Common.Interfaces.Persistence.Vehicles;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Vehicles
{
    public class TruckRepository : EntityRepository<Truck>, ITruckRepository
    {
        public TruckRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

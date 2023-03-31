using Application.Common.Interfaces.Persistence.Vehicles;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Vehicles
{
    public class VehicleRepository : EntityRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

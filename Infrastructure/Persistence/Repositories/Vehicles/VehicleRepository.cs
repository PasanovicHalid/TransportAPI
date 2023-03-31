using Application.Common.Interfaces.Persistence.Vehicles;
using Domain.Entities;
using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Vehicles
{
    public class VehicleRepository : EntityRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

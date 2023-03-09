using Application.Common.Interfaces.Persistance.Licences;
using Domain.Drivers;
using Infrastructure.Common.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Licences
{
    public class DriverLicenceRepository : EntityRepository<DriversLicence>, IDriverLicenseRepository
    {
        public DriverLicenceRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

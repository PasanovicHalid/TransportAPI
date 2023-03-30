using Application.Common.Interfaces.Persistence.Licences;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Licenses
{
    public class DriverLicenseRepository : EntityRepository<DriversLicense>, IDriverLicenseRepository
    {
        public DriverLicenseRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

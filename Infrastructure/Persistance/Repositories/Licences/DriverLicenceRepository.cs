using Application.Common.Interfaces.Persistance.Licences;
using Domain;
using Infrastructure.Common.Persistance;

namespace Infrastructure.Persistance.Repositories.Licences
{
    public class DriverLicenceRepository : EntityRepository<DriversLicence>, IDriverLicenseRepository
    {
        public DriverLicenceRepository(TransportDbContext db) : base(db)
        {
        }
    }
}

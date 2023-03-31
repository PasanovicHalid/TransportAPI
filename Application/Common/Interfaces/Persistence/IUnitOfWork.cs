using System.Data;
using Application.Common.Interfaces.Persistence.Companies;
using Application.Common.Interfaces.Persistence.Employees;
using Application.Common.Interfaces.Persistence.Licences;
using Application.Common.Interfaces.Persistence.Trailers;
using Application.Common.Interfaces.Persistence.Transportations;
using Application.Common.Interfaces.Persistence.Vehicles;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        public Task SaveAsync(CancellationToken cancellationToken = default);

        public IDbTransaction BeginTransaction();

        public IUserRepository Users { get; }

        public IEmployeeRepository Employees { get; }

        public IAdministratorRepository Administrators { get; }

        public IDriverRepository Drivers { get; }

        public IDriverLicenseRepository DriverLicenses { get; }

        public ICompanyRepository Companies { get; }

        public ITrailerRepository Trailers { get; }

        public ITransportationRepository Transportations { get; }

        public IStopRepository Stops { get; }

        public IVehicleRepository Vehicles { get; }

        public IVanRepository Vans { get; }

        public ITruckRepository Trucks { get; }

    }
}

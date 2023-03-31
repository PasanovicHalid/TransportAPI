using System.Data;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Companies;
using Application.Common.Interfaces.Persistence.Employees;
using Application.Common.Interfaces.Persistence.Licences;
using Application.Common.Interfaces.Persistence.Trailers;
using Application.Common.Interfaces.Persistence.Transportations;
using Application.Common.Interfaces.Persistence.Vehicles;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Companies;
using Infrastructure.Persistence.Repositories.Employees;
using Infrastructure.Persistence.Repositories.Licenses;
using Infrastructure.Persistence.Repositories.Trailers;
using Infrastructure.Persistence.Repositories.Transportations;
using Infrastructure.Persistence.Repositories.Vehicles;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportDbContext _context;
        private IUserRepository? _userRepository;
        private IEmployeeRepository? _employeeRepository;
        private IDriverRepository? _driverRepository;
        private IDriverLicenseRepository? _driverLicenceRepository;
        private ICompanyRepository? _companyRepository;
        private IAdministratorRepository? _administratorRepository;
        private ITrailerRepository? _trailerRepository;
        private ITransportationRepository? _transportationRepository;
        private IStopRepository? _stopRepository;
        private IVehicleRepository? _vehicleRepository;
        private IVanRepository? _vanRepository;
        private ITruckRepository? _truckRepository;

        public UnitOfWork(TransportDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context);

        public IDriverRepository Drivers => _driverRepository ??= new DriverRepository(_context);

        public IDriverLicenseRepository DriverLicenses => _driverLicenceRepository ??= new DriverLicenseRepository(_context);

        public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(_context);

        public IAdministratorRepository Administrators => _administratorRepository ??= new AdministratorRepository(_context);

        public ITrailerRepository Trailers => _trailerRepository ??= new TrailerRepository(_context);

        public ITransportationRepository Transportations => _transportationRepository ??= new TransportationRepository(_context);

        public IStopRepository Stops => _stopRepository ??= new StopRepository(_context);

        public IVehicleRepository Vehicles => _vehicleRepository ??= new VehicleRepository(_context);

        public IVanRepository Vans => _vanRepository ??= new VanRepository(_context);

        public ITruckRepository Trucks => _truckRepository ??= new TruckRepository(_context);

        public IDbTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction().GetDbTransaction();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

using System.Data;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Companies;
using Application.Common.Interfaces.Persistence.Employees;
using Application.Common.Interfaces.Persistence.Licences;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Companies;
using Infrastructure.Persistence.Repositories.Employees;
using Infrastructure.Persistence.Repositories.Licenses;
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

        public UnitOfWork(TransportDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context);

        public IDriverRepository Drivers => _driverRepository ??= new DriverRepository(_context);

        public IDriverLicenseRepository DriverLicenses => _driverLicenceRepository ??= new DriverLicenseRepository(_context);

        public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(_context);

        public IDbTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction().GetDbTransaction();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

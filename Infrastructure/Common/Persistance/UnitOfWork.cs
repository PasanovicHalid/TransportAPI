using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Persistance.Companies;
using Application.Common.Interfaces.Persistance.Employees;
using Application.Common.Interfaces.Persistance.Licences;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories.Companies;
using Infrastructure.Persistance.Repositories.Employees;
using Infrastructure.Persistance.Repositories.Licences;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.Common.Persistance
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

        public IDriverLicenseRepository DriverLicenses => _driverLicenceRepository ??= new DriverLicenceRepository(_context);

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

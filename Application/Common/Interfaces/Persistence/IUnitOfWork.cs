using System.Data;
using Application.Common.Interfaces.Persistence.Companies;
using Application.Common.Interfaces.Persistence.Employees;
using Application.Common.Interfaces.Persistence.Licences;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        public void Save();

        public IDbTransaction BeginTransaction();

        public IUserRepository Users { get; }

        public IEmployeeRepository Employees { get; }

        public IDriverRepository Drivers { get; }

        public IDriverLicenseRepository DriverLicenses { get; }

        public ICompanyRepository Companies { get; }
    }
}

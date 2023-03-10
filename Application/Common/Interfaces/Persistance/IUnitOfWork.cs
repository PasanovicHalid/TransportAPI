using Application.Common.Interfaces.Persistance.Companies;
using Application.Common.Interfaces.Persistance.Employees;
using Application.Common.Interfaces.Persistance.Licences;
using System.Data;

namespace Application.Common.Interfaces.Persistance
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistance.Companies;
using Application.Common.Interfaces.Persistance.Employees;
using Application.Common.Interfaces.Persistance.Licences;

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

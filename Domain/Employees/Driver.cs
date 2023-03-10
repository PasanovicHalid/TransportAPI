using Domain.Companies;
using Domain.Constants;
using Domain.Employees;
using Microsoft.AspNetCore.Identity;

namespace Domain.Drivers
{
    public class Driver : Employee
    {
        public IEnumerable<DriversLicence> DriverLicences { get; private set; } = new List<DriversLicence>();
        public Driver(string identityId,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      ulong companyId) : base(identityId,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              companyId)
        {

        }

        public Driver(IdentityUser user,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      Company company) : base(user,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              company)
        {

        }

        public Driver(IdentityUser user,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      ulong companyId) : base(user,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              companyId)
        {

        }

        public Driver(string identityId,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      Company company) : base(identityId,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              company)
        {

        }

        protected Driver()
        {
        }
    }
}

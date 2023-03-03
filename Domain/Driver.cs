using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Driver : Employee
    {
        public IEnumerable<DriversLicence> DriverLicences { get; private set; } = new List<DriversLicence>();
        public Driver(ulong id,
                      bool deleted,
                      string identityId,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      ulong companyId) : base(id,
                                              deleted,
                                              identityId,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              companyId)
        {

        }

        public Driver(ulong id,
                      bool deleted,
                      IdentityUser user,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      Company company) : base(id,
                                              deleted,
                                              user,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              company)
        {

        }

        public Driver(ulong id,
                      bool deleted,
                      string identityId,
                      string firstName,
                      string? middleName,
                      string lastName,
                      double salary,
                      Address address,
                      Company company) : base(id,
                                              deleted,
                                              identityId,
                                              ApplicationRolesConstants.Driver,
                                              firstName,
                                              middleName,
                                              lastName,
                                              salary,
                                              address,
                                              company)
        {

        }

        protected Driver(ulong id,
                         bool deleted) : base(id,
                                              deleted)
        {
        }
    }
}

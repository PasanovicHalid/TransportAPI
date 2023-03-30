using Domain.Constants;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Driver : Employee
    {
        public IEnumerable<DriversLicense> DriversLicenses { get; private set; } = new List<DriversLicense>();

        public IEnumerable<Transportation> AssignedTransportations { get; private set; } = new List<Transportation>();

        [ForeignKey(nameof(VehicleId))]
        public Vehicle? Vehicle { get; private set; }

        public ulong? VehicleId { get; private set; }

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

﻿using Domain.Constants;
using Domain.ValueObjects;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Driver : Employee
    {
        public List<DriversLicense> DriversLicenses { get; private set; } = new();

        public List<Transportation> AssignedTransportations { get; private set; } = new();

        [ForeignKey(nameof(VehicleId))]
        public Vehicle? Vehicle { get; private set; }

        public ulong? VehicleId { get; set; }

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

        public Result AssignVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            VehicleId = vehicle.Id;
            return Result.Ok();
        }

        public Result UnassignVehicle()
        {
            Vehicle = null;
            VehicleId = null;
            return Result.Ok();
        }
    }
}

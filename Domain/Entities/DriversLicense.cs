using Domain.Common;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Owned]
    public class DriversLicense : EntityObject
    {
        public string Category { get; set; }

        public DateTime IssuedOn { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        [ForeignKey(nameof(DriverId))]
        public Driver? Driver { get; private set; }

        public ulong DriverId { get; private set; }

        public DriversLicense(string category,
                              DateTime expirationDate,
                              ulong driverId,
                              DateTime issuingDate)
        {
            Category = category;
            ExpiresAt = expirationDate;
            DriverId = driverId;
            IssuedOn = issuingDate;
        }

        public DriversLicense(string category,
                             DateTime expirationDate,
                             Driver driver,
                             DateTime issuingDate)
        {
            Category = category;
            ExpiresAt = expirationDate;
            Driver = driver;
            DriverId = driver.Id;
            IssuedOn = issuingDate;
        }

        protected DriversLicense()
        {
        }

        public Result ChangeIssuingDate(DateTime newIssuingDate)
        {
            IssuedOn = newIssuingDate;
            return ValidateObject();
        }

        public Result ChangeExpirationDate(DateTime newExpirationDate)
        {
            ExpiresAt = newExpirationDate;
            return ValidateObject();
        }

        private Result ValidateObject()
        {
            Result result = new();

            if (IssuedOn >= ExpiresAt)
                result.Reasons.Add(new Error(nameof(IssuedOn), new Error("Issuing date is greater or equal to Expiration Date")));

            return result;
        }
    }
}

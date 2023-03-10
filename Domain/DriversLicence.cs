using Domain.Common;
using Domain.Employees;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Owned]
    public class DriversLicence : EntityObject
    {
        public string Category { get; set; }

        public DateTime IssuingDate { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public ulong DriverId { get; private set; }

        [ForeignKey(nameof(DriverId))]
        public Driver? Driver { get; private set; }

        public DriversLicence(string category,
                              DateTime expirationDate,
                              ulong driverId,
                              DateTime issuingDate)
        {
            Category = category;
            ExpirationDate = expirationDate;
            DriverId = driverId;
            IssuingDate = issuingDate;
        }

        public DriversLicence(string category,
                             DateTime expirationDate,
                             Driver driver,
                             DateTime issuingDate)
        {
            Category = category;
            ExpirationDate = expirationDate;
            Driver = driver;
            DriverId = driver.Id;
            IssuingDate = issuingDate;
        }

        protected DriversLicence()
        {
        }

        public void ChangeIssuingDate(DateTime newIssuingDate)
        {
            IssuingDate = newIssuingDate;
            ValidateObject();
        }

        public void ChangeExpirationDate(DateTime newExpirationDate)
        {
            ExpirationDate = newExpirationDate;
            ValidateObject();
        }

        private void ValidateObject()
        {
            if (IssuingDate >= ExpirationDate)
                ValidationResult.Reasons.Add(new Error(nameof(IssuingDate), new Error("Issuing date is greater or equal to Expiration Date")));
        }
    }
}

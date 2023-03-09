using Domain.Common;
using Domain.Drivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Owned]
    public class DriversLicence : EntityObject
    {
        public string Category { get; private set; }

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
    }
}

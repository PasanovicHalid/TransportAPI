using Domain.Common;
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

        public DateTime ExpirationDate { get; private set; }

        public ulong DriverId { get; private set; }

        [ForeignKey(nameof(DriverId))]
        public Driver? Driver { get; private set; }

        public DriversLicence(ulong id,
                              bool deleted,
                              string category,
                              DateTime expirationDate,
                              ulong driverId) : base(id, deleted)
        {
            Category = category;
            ExpirationDate = expirationDate;
            DriverId = driverId;
        }

        public DriversLicence(ulong id,
                             bool deleted,
                             string category,
                             DateTime expirationDate,
                             Driver driver) : base(id, deleted)
        {
            Category = category;
            ExpirationDate = expirationDate;
            Driver = driver;
            DriverId = driver.Id;
        }

        protected DriversLicence(ulong id,
                                 bool deleted) : base(id, deleted)
        {
        }
    }
}

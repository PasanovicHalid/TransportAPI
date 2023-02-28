using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared.ModelBase;

namespace TransportLibrary.Drivers.Model
{
    [Owned]
    public class DriversLicence : ValueObject
    {
        public string Category { get; private set; }

        public DateOnly ExpirationDate { get; private set; }

        public DriversLicence(string category, DateOnly expirationDate)
        {
            Category = category;
            ExpirationDate = expirationDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Category;
            yield return ExpirationDate;
        }
    }
}

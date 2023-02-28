using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared.ModelBase;

namespace TransportLibrary.Employees.Model
{
    [Owned]
    public class Salary : ValueObject
    {
        public float Amount { get; private set; }

        public Salary(float amount)
        {
            Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }
    }
}

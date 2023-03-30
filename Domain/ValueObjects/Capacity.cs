using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Capacity : ValueObject
    {
        public Volume Volume { get; private set; }
        public double MaxCarryWeight { get; private set; }

        public Capacity(Volume volume, double maxCarryWeight)
        {
            Volume = volume;
            MaxCarryWeight = maxCarryWeight;
        }

        protected Capacity() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Volume;
            yield return MaxCarryWeight;
        }
    }
}

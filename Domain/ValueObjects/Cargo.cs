﻿using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Cargo : ValueObject
    {
        public string Description { get; private set; }
        public double Weight { get; private set; }
        public Volume Volume { get; private set; }

        public Cargo(string description, double weight, Volume volume)
        {
            Description = description;
            Weight = weight;
            Volume = volume;
        }

        protected Cargo() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description; 
            yield return Weight; 
            yield return Volume;
        }
    }
}

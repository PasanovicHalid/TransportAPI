using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Van : Vehicle
    {
        public Capacity Capacity { get; private set; }

        public Van(Capacity capacity)
        {
            Capacity = capacity;
        }

        protected Van() { }
    }
}

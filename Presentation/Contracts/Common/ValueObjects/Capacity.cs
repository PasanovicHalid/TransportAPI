using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class Capacity
    {
        public Volume Volume { get; set; }
        public double MaxCarryWeight { get; set; }
    }
}

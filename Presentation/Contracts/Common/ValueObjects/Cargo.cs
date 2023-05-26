using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class Cargo
    {
        public string Description { get; set; }
        public double Weight { get; set; }
        public Volume Volume { get; set; }
    }
}

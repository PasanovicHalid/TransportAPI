using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class GpsCoordinate
    {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

    }
}

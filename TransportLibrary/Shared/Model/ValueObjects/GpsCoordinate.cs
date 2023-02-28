using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared.ModelBase;

namespace TransportLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class GpsCoordinate : ValueObject
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public GpsCoordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}

using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [Owned]
    public class GpsCoordinate : ValueObject
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

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

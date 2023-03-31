using Domain.Common;

namespace Domain.ValueObjects
{
    public class GpsCoordinate : ValueObject
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public GpsCoordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        protected GpsCoordinate() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}

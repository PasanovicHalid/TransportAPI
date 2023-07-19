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

        public double DistanceTo(GpsCoordinate other)
        {
            const double RadiusOfEarthInKilometers = 6371;
            double longitudeDifference = ToRadians(other.Longitude - Longitude);
            double latitudeDifference = ToRadians(other.Latitude - Latitude);

            double a = Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2) +
                Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(other.Latitude)) *
                Math.Sin(longitudeDifference / 2) * Math.Sin(longitudeDifference / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = RadiusOfEarthInKilometers * c;
            return distance;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        protected GpsCoordinate() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}

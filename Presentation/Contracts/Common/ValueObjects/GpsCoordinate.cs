using AutoMapper;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class GpsCoordinateAdapter : Profile
    {
        public GpsCoordinateAdapter()
        {
            CreateMap<GpsCoordinate, Domain.ValueObjects.GpsCoordinate>();
            CreateMap<GpsCoordinate, Domain.ValueObjects.GpsCoordinate>().ReverseMap();
        }
    }
    public class GpsCoordinate
    {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

    }
}

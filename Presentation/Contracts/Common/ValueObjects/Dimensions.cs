using AutoMapper;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class DimensionsAdapter : Profile
    {
        public DimensionsAdapter()
        {
            CreateMap<Dimensions, Domain.ValueObjects.Dimensions>();
            CreateMap<Dimensions, Domain.ValueObjects.Dimensions>().ReverseMap();
        }
    }
    public class Dimensions
    {
        public double Width { get; set; }
        public double Depth { get; set; }
    }
}

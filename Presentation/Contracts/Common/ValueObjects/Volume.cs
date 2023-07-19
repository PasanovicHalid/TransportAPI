using AutoMapper;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class VolumeAdapter : Profile
    {
        public VolumeAdapter() 
        {
            CreateMap<Volume, Domain.ValueObjects.Volume>();
            CreateMap<Volume, Domain.ValueObjects.Volume>().ReverseMap();
        }  
    }
    public class Volume
    {
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Height { get; set; }
    }
}

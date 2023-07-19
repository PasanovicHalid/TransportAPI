using AutoMapper;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class CapacityAdapter : Profile
    {
        public CapacityAdapter()
        {
            CreateMap<Capacity, Domain.ValueObjects.Capacity>();
            CreateMap<Capacity, Domain.ValueObjects.Capacity>().ReverseMap();
        }
    }

    public class Capacity
    {
        public Volume Volume { get; set; }
        public double MaxCarryWeight { get; set; }
    }
}

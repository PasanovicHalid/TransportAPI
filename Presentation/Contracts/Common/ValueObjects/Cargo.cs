using AutoMapper;
using DomainVO = Domain.ValueObjects;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class CargoAdapter : Profile
    {
        public CargoAdapter()
        {
            CreateMap<Cargo, DomainVO.Cargo>();
            CreateMap<Cargo, DomainVO.Cargo>().ReverseMap();
        }
    }
    public class Cargo
    {
        public string Description { get; set; }
        public double Weight { get; set; }
        public Volume Volume { get; set; }
    }
}

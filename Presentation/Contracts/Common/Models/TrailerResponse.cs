using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;

namespace Presentation.Contracts.Common.Models
{
    public class TrailerResponseAdapter : Profile
    {
        public TrailerResponseAdapter()
        {
            CreateMap<Trailer, TrailerResponse>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.UsedBy));
        }
    }
    public class TrailerResponse
    {
        public ulong Id { get; set; }
        public Capacity Capacity { get; set; }
        public ulong CompanyId { get; set; }
        public VehicleResponse? Vehicle { get; set; }
        public ulong? VehicleId { get; set; }
    }
}

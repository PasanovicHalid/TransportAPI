using Application.Trucks.Queries.GetPage;
using AutoMapper;

namespace Presentation.Contracts.Trucks
{
    public class TruckPageRequestAdapter : Profile
    {
        public TruckPageRequestAdapter()
        {
            CreateMap<TruckPageRequest, TruckPageQuery>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex + 1));
        }
    }
    public class TruckPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
        public bool Tracked { get; set; }
        public bool WithDeleted { get; set; }
        public List<string>? IncludeProperties { get; set; }
    }
}

using Application.Vans.Queries.GetPage;
using AutoMapper;

namespace Presentation.Contracts.Vans
{
    public class VanPageRequestAdapter : Profile
    {
        public VanPageRequestAdapter()
        {
            CreateMap<VanPageRequest, VanPageQuery>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex + 1));
        }
    }

    public class VanPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
        public bool Tracked { get; set; }
        public bool WithDeleted { get; set; }
        public List<string>? IncludeProperties { get; set; }
    }
}

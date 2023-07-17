using Application.Trailers.Queries.GetPage;
using AutoMapper;

namespace Presentation.Contracts.Trailers
{
    public class TrailerPageRequestAdapter : Profile
    {
        public TrailerPageRequestAdapter()
        {
            CreateMap<TrailerPageRequest, TrailerPageQuery>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex + 1));
        }
    }
    public class TrailerPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
        public bool Tracked { get; set; }
        public bool WithDeleted { get; set; }
        public List<string>? IncludeProperties { get; set; }
    }
}

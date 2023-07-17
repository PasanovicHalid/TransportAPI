using Application.Employees.Queries.GetPage;
using AutoMapper;

namespace Presentation.Contracts.Employees
{
    public class EmployeePageRequestAdapter : Profile
    {
        public EmployeePageRequestAdapter()
        {
            CreateMap<EmployeePageRequest, EmployeePageQuery>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex + 1));
        }
    }

    public class EmployeePageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
        public bool Tracked { get; set; }
        public bool WithDeleted { get; set; }
    }


}

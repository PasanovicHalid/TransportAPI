using Application.Drivers.Queries.GetPerformanceOfDriver;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Drivers
{
    public class GetPerformanceDataOfDriverAdapter : Profile
    {
        public GetPerformanceDataOfDriverAdapter()
        {
            CreateMap<GetPerformanceDataOfDriverRequest, GetPerformanceDataOfDriverQuery>();
        }
    }

    public class GetPerformanceDataOfDriverRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

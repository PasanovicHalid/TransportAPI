using Application.Dashboard.Queries.GetDashboardInfo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Dashboard
{
    public class DashboardAdapter : Profile
    {
        public DashboardAdapter() 
        {
            CreateMap<DashboardRequest, GetDashboardInfoQuery>();
        }

    }
    public class DashboardRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

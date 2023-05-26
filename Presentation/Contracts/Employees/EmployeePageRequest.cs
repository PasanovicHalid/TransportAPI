﻿using Application.Companies.Commands.Create;
using Application.Employees.Queries.GetPage;
using AutoMapper;
using Presentation.Contracts.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

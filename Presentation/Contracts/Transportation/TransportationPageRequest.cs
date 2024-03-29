﻿using Application.Transportations.Queries.GetPage;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Transportation
{
    public class TransportationPageAdapter : Profile
    {
        public TransportationPageAdapter()
        {
            CreateMap<TransportationPageRequest, GetTransportPageQuery>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex + 1));
        }
    }
    public class TransportationPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
        public bool Tracked { get; set; }
        public bool WithDeleted { get; set; }
        public List<string>? IncludeProperties { get; set; }
    }
}

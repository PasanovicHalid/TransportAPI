using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.Models
{
    public class TrailerResponseAdapter : Profile
    {
        public TrailerResponseAdapter()
        {
            CreateMap<Trailer, TrailerResponse>()
                .ForMember(dest => dest.Capacity, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return new Capacity
                        {
                            Volume = new Volume
                            {
                                Height = src.Capacity.Volume.Height,
                                Width = src.Capacity.Volume.Width,
                                Depth = src.Capacity.Volume.Depth
                            },
                            MaxCarryWeight = src.Capacity.MaxCarryWeight
                        };
                    });
                });
        }
    }
    public class TrailerResponse
    {
        public ulong Id { get; set; }
        public Capacity Capacity { get; set; }
        public ulong CompanyId { get; private set; }
        public ulong? VehicleId { get; set; }
    }
}

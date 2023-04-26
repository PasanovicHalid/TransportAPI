using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
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
            CreateMap<Trailer, TrailerResponse>();
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

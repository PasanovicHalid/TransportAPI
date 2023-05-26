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
    public class VehicleResponseAdapter : Profile
    {
        public VehicleResponseAdapter()
        {
            CreateMap<Vehicle, VehicleResponse>();
        }
    }
    public class VehicleResponse
    {
        public ulong Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public ulong CompanyId { get; private set; }
    }
}

using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;

namespace Presentation.Contracts.Common.Models
{
    public class VanResponseAdapter : Profile
    {
        public VanResponseAdapter()
        {
            CreateMap<Van, VanResponse>();
        }
    }

    public class VanResponse
    {
        public ulong Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public Capacity Capacity { get; set; }
        public ulong CompanyId { get; set; }
        public ulong? DriverId { get; set; }
        public EmployeeResponse? Driver { get; set; }
    }
}

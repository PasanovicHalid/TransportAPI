using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;

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
        public ulong CompanyId { get; set; }
        public ulong? DriverId { get; set; }
        public EmployeeResponse? Driver { get; set; }
    }
}

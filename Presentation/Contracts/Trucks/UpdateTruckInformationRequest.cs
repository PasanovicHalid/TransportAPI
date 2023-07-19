using Application.Trucks.Commands.UpdateInformation;
using AutoMapper;
using Domain.ValueObjects;

namespace Presentation.Contracts.Trucks
{
    public class UpdateTruckInformationRequestAdapter : Profile
    {
        public UpdateTruckInformationRequestAdapter()
        {
            CreateMap<UpdateTruckInformationRequest, UpdateTruckInformationCommand>();
        }
    }
    public class UpdateTruckInformationRequest
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
    }
}

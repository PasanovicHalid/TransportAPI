using Application.Vans.Commands.UpdateInformation;
using AutoMapper;
using Domain.ValueObjects;

namespace Presentation.Contracts.Vans
{
    public class UpdateVanInformationRequestAdapter : Profile
    {
        public UpdateVanInformationRequestAdapter()
        {
            CreateMap<UpdateVanInformationRequest, UpdateVanInformationCommand>();
        }
    }
    public class UpdateVanInformationRequest
    {
        public double Width { get; set; }
        public double Depth { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public double WidthCompartment { get; set; }
        public double DepthCompartment { get; set; }
        public double HeightCompartment { get; set; }
        public double MaxCarryWeight { get; set; }
    }
}

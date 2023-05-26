using Application.Vans.Commands.UpdateInformation;
using AutoMapper;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Vans
{
    public class UpdateVanInformationRequestAdapter : Profile
    {
        public UpdateVanInformationRequestAdapter()
        {
            CreateMap<UpdateVanInformationRequest, UpdateVanInformationCommand>()
                .ForMember(dest => dest.Dimensions, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return new Dimensions(src.Width,
                                              src.Depth);
                    });
                })
                .ForMember(dest => dest.Capacity, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return new Capacity(new Volume(src.WidthCompartment,
                                                       src.DepthCompartment,
                                                       src.HeightCompartment), src.MaxCarryWeight);
                    });
                });
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

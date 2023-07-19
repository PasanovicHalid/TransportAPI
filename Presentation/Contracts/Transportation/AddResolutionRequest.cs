using Application.Transportations.Commands.AddResolution;
using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Transportation
{
    public class AddResolutionRequestAdapter : Profile
    {
        public AddResolutionRequestAdapter()
        {
            CreateMap<AddResolutionRequest, AddResolutionToTransportationCommand>();
        }
    }   
    public class AddResolutionRequest
    {
        public ulong DriverId { get; set; }
        public Money Cost { get; set; }
        public GpsCoordinate StartLocation { get; set; }
    }
}

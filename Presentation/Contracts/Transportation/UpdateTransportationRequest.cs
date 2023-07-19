using Application.Transportations.Commands.Create;
using Application.Transportations.Commands.Update;
using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Transportation
{
    public class UpdateTransportationRequestAdapter : Profile
    {
        public UpdateTransportationRequestAdapter()
        {
            CreateMap<UpdateTransportationRequest, UpdateTransportationCommand>();
        }
    }
    public class UpdateTransportationRequest
    {
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public Cargo Cargo { get; set; }
        public Money Received { get; set; }
        public Address Destination { get; set; } = new();
    }
}

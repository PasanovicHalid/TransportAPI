using Application.Transportations.Commands.Create;
using AutoMapper;
using Presentation.Contracts.Common.Models;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Transportation
{
    public class CreateTransportationRequestAdapter : Profile
    {
        public CreateTransportationRequestAdapter()
        {
            CreateMap<CreateTransportationRequest, CreateTransportationCommand>();
        }
    }
    public class CreateTransportationRequest
    {
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public Cargo Cargo { get; set; }
        public Money Received { get; set; }
        public Address Destination { get; set; } = new();
    }
}

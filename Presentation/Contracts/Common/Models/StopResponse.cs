using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.Models
{

    public class StopResponseAdapter : Profile
    {
        public StopResponseAdapter()
        {
            CreateMap<Domain.Entities.Stop, StopResponse>();
        }
    }

    public class StopResponse
    {
        public Address Destination { get; set; }

        public ulong TransportatioId { get; set; }
    }
}

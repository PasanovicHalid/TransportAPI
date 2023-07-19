using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.Models
{
    public class CostResponseAdapter : Profile
    {
        public CostResponseAdapter()
        {
            CreateMap<Domain.Entities.Cost, CostResponse>();
        }
    }

    public class CostResponse
    {
        public StopResponse From { get; set; }
        public StopResponse To { get; set; }
        public ulong FromId { get; set; }
        public ulong ToId { get; set; }
        public Money Expendature { get; set; }
    }
}

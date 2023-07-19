using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.Models
{
    public class TransportationResponseAdapter : Profile
    {
        public TransportationResponseAdapter() 
        { 
            CreateMap<Domain.Entities.Transportation, TransportationResponse>();
        }
    }
    public class TransportationResponse
    {
        public ulong Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime RequiredFor { get; set; }
        public Cargo Transporting { get; set; }
        public Address Destination { get; set; }
        public Money Cost { get; set; }
        public Money Received { get; set; }
        public GpsCoordinate StartLocation { get; set; }
        public EmployeeResponse DrivenBy { get; set; }
        public CompanyResponse DesignatedTo { get; set; }
        public ulong DriverId { get; set; }
        public ulong CompanyId { get; set; }

    }

}

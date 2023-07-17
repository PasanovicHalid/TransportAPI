using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;

namespace Presentation.Contracts.Common.Models
{
    public class CompanyResponseAdapter : Profile
    {
        public CompanyResponseAdapter()
        {
            CreateMap<Company, CompanyResponse>();
        }
    }
    public class CompanyResponse
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<VehicleResponse> Vehicles { get; set; }
        public List<EmployeeResponse> Employees { get; set; }
        public List<TrailerResponse> Trailers { get; set; }
    }
}

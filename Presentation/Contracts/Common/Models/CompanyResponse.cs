using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Companies.Commands.Create;
using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;
using Presentation.Contracts.Companies;

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

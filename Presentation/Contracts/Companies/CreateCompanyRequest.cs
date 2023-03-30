using Application.Companies.Commands.Create;
using AutoMapper;

namespace Presentation.Contracts.Companies
{
    public class CreateCompanyAdapter : Profile
    {
        public CreateCompanyAdapter()
        {
            CreateMap<CreateCompanyRequest, CreateCompanyCommand>();
        }
    }
    public class CreateCompanyRequest
    {
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}

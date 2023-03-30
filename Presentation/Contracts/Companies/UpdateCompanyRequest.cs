using Application.Companies.Commands.Create;
using Application.Companies.Commands.UpdateInformation;
using AutoMapper;

namespace Presentation.Contracts.Companies
{
    public class UpdateCompanyAdapter : Profile
    {
        public UpdateCompanyAdapter()
        {
            CreateMap<UpdateCompanyRequest, UpdateCompanyInformationCommand>();
        }
    }
    public class UpdateCompanyRequest
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}

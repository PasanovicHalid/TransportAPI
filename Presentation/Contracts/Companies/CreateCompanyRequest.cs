using Application.Companies.Commands.Create;
using Application.Companies.Commands.Update;
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
    }
}

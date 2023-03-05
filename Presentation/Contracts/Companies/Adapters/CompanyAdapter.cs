using Application.Companies.Commands.Create;
using Application.Companies.Commands.Update;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Companies.Adapters
{
    public class CompanyAdapter : Profile
    {
        public CompanyAdapter()
        {
            CreateMap<CreateCompanyRequest, CreateCompanyCommand>();
            CreateMap<UpdateCompanyRequest, UpdateCompanyCommand>();
        }
    }
}

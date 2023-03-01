using Application.Authentication.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication.Adapters
{
    public class AuthenticationAdapter : Profile
    {
        public AuthenticationAdapter()
        {
            CreateMap<AuthenticationResult, AutheticationResponse>()
                .ForMember(dest => dest.ValidUntil, src => src.MapFrom(par => par.ExpirationDate));
        }
    }
}

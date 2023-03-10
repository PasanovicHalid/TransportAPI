using Application.Authentication.Contracts;
using AutoMapper;

namespace Presentation.Contracts.Authentication
{
    public class AuthenticationAdapter : Profile
    {
        public AuthenticationAdapter()
        {
            CreateMap<AuthenticationResult, AutheticationResponse>()
                .ForMember(dest => dest.ValidUntil, src => src.MapFrom(par => par.ExpirationDate));
        }
    }

    public class AutheticationResponse
    {
        public string Token { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}

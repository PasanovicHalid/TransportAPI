using Application.Authentication.Queries.Login;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Authentication
{
    public class LoginAdapter : Profile
    {
        public LoginAdapter()
        {
            CreateMap<LoginRequest, LoginQuery>();
        }
    }
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

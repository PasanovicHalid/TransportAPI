using Application.Authentication.Commands.Register.SuperAdmins;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Authentication
{
    public class RegisterAdapter : Profile
    {
        public RegisterAdapter()
        {
            CreateMap<RegistrationRequest, RegisterSuperAdminCommand>();
        }
    }

    public class RegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}

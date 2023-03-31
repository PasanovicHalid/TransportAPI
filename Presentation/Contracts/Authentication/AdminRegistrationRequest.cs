using Application.Authentication.Commands.Register.Admins;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Authentication
{
    public class AdminRegistrationAdapter : Profile
    {
        public AdminRegistrationAdapter()
        {
            CreateMap<AdminRegistrationRequest, RegisterAdminCommand>();
        }
    }

    public class AdminRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public double Salary { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}

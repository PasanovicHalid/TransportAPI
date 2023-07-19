using Application.Authentication.Commands.Register.Drivers;
using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Authentication
{
    public class DriverRegistrationAdapter : Profile
    {
        public DriverRegistrationAdapter()
        {
            CreateMap<DriverRegistrationRequest, RegisterDriverCommand>();
        }
    }

    public class DriverRegistrationRequest
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

        public Address Address { get; set; }
    }
}

using Application.Authentication.Commands.Register.Admins;
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
            CreateMap<DriverRegistrationRequest, RegisterDriverCommand>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Domain.ValueObjects.Address(src.Address.Street,
                                                                                                           src.Address.City,
                                                                                                           src.Address.State,
                                                                                                           src.Address.PostalCode,
                                                                                                           src.Address.Country)))
                .ForPath(dest => dest.Address.GpsCoordinate, opt =>
                {
                    opt.Condition(src => src.Source.Address.GpsCoordinate != null);
                    opt.MapFrom(src => new Domain.ValueObjects.GpsCoordinate(src.Address.GpsCoordinate!.Longitude!.Value,
                                                                             src.Address.GpsCoordinate!.Latitude!.Value));
                });
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

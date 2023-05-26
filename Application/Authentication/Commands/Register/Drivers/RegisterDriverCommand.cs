using Application.Authentication.Contracts;
using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Authentication.Commands.Register.Drivers
{
    public class RegisterDriverCommand : IRequest<Result<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class RegisterDriverValidator : AbstractValidator<RegisterDriverCommand>
    {
        public RegisterDriverValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress();

            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Salary).NotEmpty()
                                  .GreaterThan(0);

            RuleFor(x => x.Address).NotNull();

            RuleFor(x => x.Address.Street).NotEmpty();

            RuleFor(x => x.Address.City).NotEmpty();

            RuleFor(x => x.Address.State).NotEmpty();

            RuleFor(x => x.Address.PostalCode).NotEmpty();

            RuleFor(x => x.Address.Country).NotEmpty();

            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.Address.GpsCoordinate!.Latitude)
                .NotNull()
                .When(x => x.Address.GpsCoordinate is not null);
            RuleFor(x => x.Address.GpsCoordinate!.Longitude)
                .NotNull()
                .When(x => x.Address.GpsCoordinate is not null);
        }
    }
}

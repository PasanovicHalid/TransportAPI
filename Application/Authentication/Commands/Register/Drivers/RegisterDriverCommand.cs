using Application.Authentication.Contracts;
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
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
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

            RuleFor(x => x.Street).NotEmpty();

            RuleFor(x => x.City).NotEmpty();

            RuleFor(x => x.State).NotEmpty();

            RuleFor(x => x.PostalCode).NotEmpty();

            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

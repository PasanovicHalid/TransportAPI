using Application.Authentication.Commands.Register.SuperAdmin;
using Application.Authentication.Contracts;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register.Admin
{
    public class RegisterAdminCommand : IRequest<Result<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public string FirstName { get; private set; }

        public string? MiddleName { get; private set; }

        public string LastName { get; private set; }

        public double Salary { get; private set; }

        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string PostalCode { get; private set; }

        public string Country { get; private set; }

        public ulong CompanyId { get; private set; }
    }

    public class RegisterAdminValidator : AbstractValidator<RegisterAdminCommand>
    {
        public RegisterAdminValidator()
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

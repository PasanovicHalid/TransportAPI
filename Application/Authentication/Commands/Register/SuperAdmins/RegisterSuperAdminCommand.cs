using Application.Authentication.Contracts;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Authentication.Commands.Register.SuperAdmins
{
    public class RegisterSuperAdminCommand : IRequest<Result<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RegisterSuperAdminValidator : AbstractValidator<RegisterSuperAdminCommand>
    {
        public RegisterSuperAdminValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress();

            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

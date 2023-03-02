using Domain.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress();

            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.UserType).NotEmpty()
                                    .Must(r => ApplicationRolesConstants.Roles.Contains(r))
                                    .WithMessage("User Type passed doesn't exist");

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

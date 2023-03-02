using Application.Authentication.Commands.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

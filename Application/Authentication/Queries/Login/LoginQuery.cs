﻿using Application.Authentication.Contracts;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<Result<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

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

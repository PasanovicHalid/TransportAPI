﻿using Application.Authentication.Commands.Register.SuperAdmin;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Commands.Create
{
    public class CreateCompanyCommand : IRequest<Result>
    {
        public string Name { get; set; } = null!;
    }

    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}

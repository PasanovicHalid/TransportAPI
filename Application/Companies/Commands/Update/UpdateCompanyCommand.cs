using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Commands.Update
{
    public class UpdateCompanyCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand> 
    {
        public UpdateCompanyValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }


}

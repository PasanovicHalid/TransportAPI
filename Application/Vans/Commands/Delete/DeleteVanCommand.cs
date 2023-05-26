using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vans.Commands.Delete
{
    public class DeleteVanCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class DeleteVanValidator : AbstractValidator<DeleteVanCommand>
    {
        public DeleteVanValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

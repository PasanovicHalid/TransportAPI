using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Commands.Delete
{
    public class DeleteTransportationCommand : IRequest<Result>
    {
        public ulong TransportationId { get; set; }

        public ulong CompanyId { get; set; }
    }
    
    public class DeleteTransportationValidator : AbstractValidator<DeleteTransportationCommand>
    {
        public DeleteTransportationValidator()
        {
            RuleFor(x => x.TransportationId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

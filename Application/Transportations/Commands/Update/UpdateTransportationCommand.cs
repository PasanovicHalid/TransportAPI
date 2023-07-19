using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Commands.Update
{
    public class UpdateTransportationCommand : IRequest<Result>
    {
        public ulong TransportationId { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public Cargo Cargo { get; set; }
        public ulong CompanyId { get; set; }
        public Money Received { get; set; }
        public Address Destination { get; set; }
    }

    public class UpdateTransportationValidator : AbstractValidator<UpdateTransportationCommand>
    {
        public UpdateTransportationValidator()
        {
            RuleFor(x => x.TransportationId).NotEmpty();
            RuleFor(x => x.DateOfDeparture).NotEmpty();
            RuleFor(x => x.DateOfArrival).NotEmpty();
            RuleFor(x => x.Cargo).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Received).NotEmpty();
            RuleFor(x => x.Destination).NotEmpty();
        }
    }
}

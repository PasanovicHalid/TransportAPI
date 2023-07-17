using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Transportations.Commands.Create
{
    public class CreateTransportationCommand : IRequest<Result>
    {
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public Cargo Cargo { get; set; }
        public ulong CompanyId { get; set; }
        public Money Received { get; set; }
        public List<Stop> Stops { get; set; } = new();
    }

    public class CreateTransportationValidator : AbstractValidator<CreateTransportationCommand>
    {
        public CreateTransportationValidator()
        {
            RuleFor(x => x.DateOfDeparture).NotEmpty();
            RuleFor(x => x.DateOfArrival).NotEmpty();
            RuleFor(x => x.Cargo).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Received).NotEmpty();
            RuleFor(x => x.Stops).NotEmpty();
        }
    }
}

using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Transportations.Commands.AddResolution
{
    public class AddResolutionToTransportationCommand : IRequest<Result>
    {
        public ulong TransportationId { get; set; }
        public ulong DriverId { get; set; }
        public List<Cost> Costs { get; set; }
    }

    public class AddResolutionToTransportationValidator : AbstractValidator<AddResolutionToTransportationCommand>
    {
        public AddResolutionToTransportationValidator()
        {
            RuleFor(x => x.TransportationId).NotEmpty();
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.Costs).NotEmpty();
        }
    }
}

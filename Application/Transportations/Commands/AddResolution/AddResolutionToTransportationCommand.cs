using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Transportations.Commands.AddResolution
{
    public class AddResolutionToTransportationCommand : IRequest<Result>
    {
        public ulong CompanyId { get; set; }
        public ulong TransportationId { get; set; }
        public ulong DriverId { get; set; }
        public Money Cost { get; set; }
        public GpsCoordinate StartLocation { get; set; }
    }

    public class AddResolutionToTransportationValidator : AbstractValidator<AddResolutionToTransportationCommand>
    {
        public AddResolutionToTransportationValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.TransportationId).NotEmpty();
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.Cost).NotEmpty();
            RuleFor(x => x.StartLocation).NotEmpty();
        }
    }
}

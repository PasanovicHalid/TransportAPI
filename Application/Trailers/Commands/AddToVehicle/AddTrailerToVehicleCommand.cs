using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trailers.Commands.AddToVehicle
{
    public class AddTrailerToVehicleCommand : IRequest<Result>
    {
        public ulong VehicleId { get; set; }
        public ulong TrailerId { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class AddTrailerToVehicleValidator : AbstractValidator<AddTrailerToVehicleCommand>
    {
        public AddTrailerToVehicleValidator()
        {
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.TrailerId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }

}
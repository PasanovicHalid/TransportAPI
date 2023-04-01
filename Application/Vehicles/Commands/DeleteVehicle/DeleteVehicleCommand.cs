using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<Result>
    {
        public ulong VehicleId { get; set; }

        public ulong CompanyId { get; set; }
    }

    public class DeleteVehicleValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleValidator()
        {
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }

}



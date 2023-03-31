using MediatR;
using FluentResults;
using FluentValidation;

namespace Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<Result>
    {
        public ulong VehicleId { get; set; }

        public string AdminIdentityId { get; set; }
    }

    public class DeleteVehicleValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleValidator()
        {
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.AdminIdentityId).NotEmpty();
        }
    }

}



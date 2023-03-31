using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Common.Errors;

namespace Application.Vehicles.Commands.UpdateInformation
{
    public class UpdateVehicleInformationCommandHandler : IRequestHandler<UpdateVehicleInformationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleInformationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateVehicleInformationCommand request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(v => v.Id == request.Id && v.OwnedBy.Employees.Any(e => e.IdentityId == request.AdminIdentityId), cancellationToken: cancellationToken);

            if (vehicle == null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Vehicle)));

            SetupVehicle(request, vehicle);

            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();

        }

        private static void SetupVehicle(UpdateVehicleInformationCommand request, Vehicle? vehicle)
        {
            vehicle.Manufacturer = request.Manufacturer;
            vehicle.Model = request.Model;
            vehicle.DateOfManufacturing = request.DateOfManufacturing;
            vehicle.Dimensions = request.Dimensions;
        }
    }
}


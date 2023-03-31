using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Vehicles.Errors;

namespace Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(v => v.Id == request.VehicleId && v.OwnedBy.Employees.Any(e => e.IdentityId == request.AdminIdentityId), includeProperties: new List<string> { "Trailers" }, cancellationToken: cancellationToken);

            if (vehicle == null)
                return Result.Fail(new VehicleDoesntBelongToCompanyOrDoesntExist());

            vehicle.Trailers.ForEach(t => t.VehicleId = null);

            _unitOfWork.Vehicles.Remove(vehicle);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}


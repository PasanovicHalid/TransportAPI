using Application.Common.Interfaces.Persistence;
using Application.Vehicles.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;

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
            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(v => v.Id == request.VehicleId && v.CompanyId == request.CompanyId,
                                                                                 includeProperties: new List<string> { "Trailers" },
                                                                                 cancellationToken: cancellationToken);

            if (vehicle == null)
                return Result.Fail(new VehicleDoesntBelongToCompanyOrDoesntExist());

            vehicle.Trailers.ForEach(t => t.VehicleId = null);

            _unitOfWork.Vehicles.Remove(vehicle);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}


using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trailers.Commands.AddToVehicle
{
    public class AddTrailerToVehicleCommandHandler : IRequestHandler<AddTrailerToVehicleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTrailerToVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTrailerToVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(v => v.Id == request.VehicleId && v.CompanyId == request.CompanyId,
                                                                                 cancellationToken: cancellationToken);

            if (vehicle == null)
                return Result.Fail(new EntityDoesntExist(request.VehicleId, nameof(Vehicle)));

            Trailer? trailer = await _unitOfWork.Trailers.GetFirstOrDefaultAsync(t => t.Id == request.TrailerId && t.CompanyId == request.CompanyId,
                                                                                 cancellationToken: cancellationToken);

            if (trailer == null)
                return Result.Fail(new EntityDoesntExist(request.TrailerId, nameof(Trailer)));

            vehicle.Trailers.Add(trailer);

            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
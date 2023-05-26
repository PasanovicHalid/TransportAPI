using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trucks.Commands.Delete
{
    public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruckCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTruckCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            Truck? truck = await _unitOfWork.Trucks.GetFirstOrDefaultAsync(t => t.Id == request.Id && t.CompanyId == request.CompanyId,
                                                                                          cancellationToken: cancellationToken, includeProperties: new List<string>() { "Trailers" });

            if (truck is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Truck)));

            foreach (Trailer trailer in truck.Trailers)
                trailer.VehicleId = null;

            truck.DriverId = null;

            _unitOfWork.Trucks.Remove(truck);

            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

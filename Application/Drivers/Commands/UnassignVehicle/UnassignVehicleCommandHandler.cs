using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Drivers.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Commands.UnassignVehicle
{
    public class UnassignVehicleCommandHandler : IRequestHandler<UnassignVehicleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnassignVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UnassignVehicleCommand request, CancellationToken cancellationToken)
        {
            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(x => x.Id == request.DriverId && x.CompanyId == request.CompanyId,
                                                                                         cancellationToken: cancellationToken);
            if (driver is null)
                return Result.Fail(new EntityDoesntExist(request.DriverId, nameof(Driver)));

            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(x => x.Id == driver.VehicleId && x.CompanyId == request.CompanyId,
                                                                                                        cancellationToken: cancellationToken);

            if (vehicle is null)
                return Result.Fail(new VehicleForUnassigningDoesntExist());

            Result result = driver.UnassignVehicle();

            if (result.IsFailed)
                return result;

            result = vehicle.UnassignDriver();

            if (result.IsFailed)
                return result;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Vehicles.Update(vehicle);

            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}

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

namespace Application.Drivers.Commands.AssignVehicle
{
    public class AssignVehicleCommandHandler : IRequestHandler<AssignVehicleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AssignVehicleCommand request, CancellationToken cancellationToken)
        {
            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(x => x.Id == request.DriverId && x.CompanyId == request.CompanyId,
                                                                          cancellationToken: cancellationToken,
                                                                          includeProperties: new() { "Vehicle" });
            if (driver is null)
                return Result.Fail(new EntityDoesntExist(request.DriverId, nameof(Driver)));

            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(x => x.Id == request.VehicleId && x.CompanyId == request.CompanyId,
                                                                          cancellationToken: cancellationToken);
            if (vehicle is null)
                return Result.Fail(new EntityDoesntExist(request.VehicleId, nameof(Vehicle)));

            Result result;

            if (driver.VehicleId is not null)
            {
                result = driver.Vehicle!.UnassignDriver();

                if (result.IsFailed)
                    return result;

                _unitOfWork.Vehicles.Update(driver.Vehicle);
            }
                

            result = driver.AssignVehicle(vehicle);

            if (result.IsFailed)
                return result;

            result = vehicle.AssignDriver(driver);

            if (result.IsFailed)
                return result;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Vehicles.Update(vehicle);

            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}

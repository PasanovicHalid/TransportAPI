using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Queries.GetDriversWithoutVehicles
{
    public class GetDriversWithoutVehiclesQueryHandler : IRequestHandler<GetDriversWithoutVehiclesQuery, Result<List<Driver>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDriversWithoutVehiclesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Driver>>> Handle(GetDriversWithoutVehiclesQuery request, CancellationToken cancellationToken)
        {
            List<Driver> drivers = await _unitOfWork.Drivers.GetAllAsync(x => x.CompanyId == request.CompanyId && x.VehicleId == null,
                                                                                                      cancellationToken: cancellationToken);

            return Result.Ok(drivers);
        }
    }
}

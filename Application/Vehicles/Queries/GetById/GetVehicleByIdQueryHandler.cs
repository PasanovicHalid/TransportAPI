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

namespace Application.Vehicles.Queries.GetById
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<Vehicle>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVehicleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Vehicle>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _unitOfWork.Vehicles.GetFirstOrDefaultAsync(x => x.Id == request.VehicleId && x.CompanyId == request.CompanyId,
                                                                                 cancellationToken: cancellationToken);
            if (vehicle is null)
                return Result.Fail(new EntityDoesntExist(request.VehicleId, nameof(Vehicle)));

            return Result.Ok(vehicle);
        }
    }
}

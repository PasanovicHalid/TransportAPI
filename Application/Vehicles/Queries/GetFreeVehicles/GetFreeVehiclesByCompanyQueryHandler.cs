using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetFreeVehicles
{
    public class GetFreeVehiclesByCompanyQueryHandler : IRequestHandler<GetFreeVehiclesByCompanyQuery, Result<List<Vehicle>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFreeVehiclesByCompanyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Vehicle>>> Handle(GetFreeVehiclesByCompanyQuery request, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await _unitOfWork.Vehicles.GetAllAsync(x => x.CompanyId == request.CompanyId && x.DriverId == null,
                                                                            cancellationToken: cancellationToken);

            return Result.Ok(vehicles);
        }
    }
}

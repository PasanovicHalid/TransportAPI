using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetAllByCompany
{
    public class GetAllVehiclesByCompanyQueryHandler : IRequestHandler<GetAllVehiclesByCompanyQuery, Result<List<Vehicle>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVehiclesByCompanyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Vehicle>>> Handle(GetAllVehiclesByCompanyQuery request, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await _unitOfWork.Vehicles.GetAllAsync(x => x.CompanyId == request.CompanyId,
                                                                                           cancellationToken: cancellationToken);

            return Result.Ok(vehicles);
        }
    }
}

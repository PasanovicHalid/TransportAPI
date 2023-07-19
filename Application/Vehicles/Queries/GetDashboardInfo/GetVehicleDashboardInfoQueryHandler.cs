using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetDashboardInfo
{
    public class GetVehicleDashboardInfoQueryHandler : IRequestHandler<GetVehicleDashboardInfoQuery, Result<VehicleDashboardInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVehicleDashboardInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VehicleDashboardInfo>> Handle(GetVehicleDashboardInfoQuery request, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await _unitOfWork.Vehicles.GetAllAsync(v => v.CompanyId == request.CompanyId,
                                                                            cancellationToken: cancellationToken);

            List<Trailer> trailers = await _unitOfWork.Trailers.GetAllAsync(t => t.CompanyId == request.CompanyId,
                                                                                           cancellationToken: cancellationToken);

            VehicleDashboardInfo vehicleDashboardInfo = new VehicleDashboardInfo();

            for(int i = 0; i < Math.Max(vehicles.Count, trailers.Count); i++)
            {
                if (i < trailers.Count)
                    vehicleDashboardInfo.TotalTrailers++;

                if (i < vehicles.Count)
                {
                    if (vehicles[i] is Van)
                        vehicleDashboardInfo.TotalVans++;
                    else if (vehicles[i] is Truck)
                        vehicleDashboardInfo.TotalTrucks++;
                }
            }

            return Result.Ok(vehicleDashboardInfo);
        }
    }
}

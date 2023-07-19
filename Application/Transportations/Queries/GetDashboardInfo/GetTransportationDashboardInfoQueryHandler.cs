using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Queries.GetDashboardInfo
{
    public class GetTransportationDashboardInfoQueryHandler : IRequestHandler<GetTransportationDashboardInfoQuery, Result<TransportationDashboardInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransportationDashboardInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransportationDashboardInfo>> Handle(GetTransportationDashboardInfoQuery request, CancellationToken cancellationToken)
        {
            List<Transportation> transportations = await _unitOfWork.Transportations.GetAllAsync(x => x.CompanyId == request.CompanyId);

            TransportationDashboardInfo dashboardInfo = new TransportationDashboardInfo();

            foreach (var transportation in transportations)
            {
                if(transportation.DriverId is null)
                    dashboardInfo.TotalPendingTransportations++;
                else
                    dashboardInfo.TotalCompletedTransportations++;

                Result<double> distance = transportation.GetDistanceToDestination();

                if (distance.IsSuccess)
                    dashboardInfo.TotalKilometersDriven += distance.Value;
            }

            return Result.Ok(dashboardInfo);
        }

    }
    
}

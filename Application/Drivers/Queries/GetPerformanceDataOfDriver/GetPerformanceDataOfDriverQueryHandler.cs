using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Dashboard.Queries.GetDashboardInfo;
using Application.Drivers.Queries.GetPerformanceOfDriver;
using Domain.Entities;
using Domain.PlainObjects;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Queries.GetPerformanceDataOfDriver
{
    public class GetPerformanceDataOfDriverQueryHandler : IRequestHandler<GetPerformanceDataOfDriverQuery, Result<DriverPerformanceData>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPerformanceDataOfDriverQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DriverPerformanceData>> Handle(GetPerformanceDataOfDriverQuery request, CancellationToken cancellationToken)
        {
            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(x => x.Id == request.DriverId 
                                                                              && x.CompanyId == request.CompanyId,                                                                                  
                                                                              cancellationToken: cancellationToken);
            if (driver is null)
                return Result.Fail(new EntityDoesntExist(request.DriverId, nameof(Driver)));

            List<Transportation> transportationsForRange = await _unitOfWork.Transportations.GetAllAsync(x => x.CompanyId == request.CompanyId 
                                                                                                        && x.DriverId == request.DriverId 
                                                                                                        && x.RequiredFor <= request.EndDate 
                                                                                                        && x.RequiredFor >= request.StartDate,
                                                                                                        orderBy: x => x.RequiredFor,
                                                                                                        cancellationToken: cancellationToken);

            DriverPerformanceData driverPerformanceData = CreateChartData(request, transportationsForRange);

            return Result.Ok(driverPerformanceData);
        }

        private static DriverPerformanceData CreateChartData(GetPerformanceDataOfDriverQuery request, List<Transportation> transportationsForRange)
        {
            DriverPerformanceData driverPerformanceData = new();

            int daysBetweenStartAndEnd = (request.EndDate - request.StartDate).Days;
            Dictionary<DateTime, List<Transportation>> transportationsByDateForCurrentMonth = new(daysBetweenStartAndEnd);

            foreach (Transportation transportation in transportationsForRange)
            {
                if (transportationsByDateForCurrentMonth.ContainsKey(transportation.RequiredFor.Date))
                    transportationsByDateForCurrentMonth[transportation.RequiredFor.Date].Add(transportation);
                else
                    transportationsByDateForCurrentMonth.Add(transportation.RequiredFor.Date, new List<Transportation> { transportation });
            }

            for (int i = 0; i < daysBetweenStartAndEnd; i++)
            {
                transportationsByDateForCurrentMonth.TryGetValue(request.StartDate.AddDays(i).Date, out List<Transportation>? transportationsForDay);

                if (transportationsForDay is null)
                {
                    driverPerformanceData.NumberOfTransportations.Add(new ChartDatapoint { X = i + 1, Y = 0 });
                    continue;
                }

                driverPerformanceData.NumberOfTransportations.Add(new ChartDatapoint { X = i + 1, Y = transportationsForDay.Count });
            }

            return driverPerformanceData;
        }
    }
}

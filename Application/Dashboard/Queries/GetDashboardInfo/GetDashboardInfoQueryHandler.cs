using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.PlainObjects;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.Queries.GetDashboardInfo
{
    public class GetDashboardInfoQueryHandler : IRequestHandler<GetDashboardInfoQuery, Result<DashboardInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDashboardInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardInfo>> Handle(GetDashboardInfoQuery request, CancellationToken cancellationToken)
        {
            DashboardInfo dashboardInfo = new();

            List<Transportation> transportationsForRange = await _unitOfWork.Transportations.GetAllAsync(x => x.CompanyId == request.CompanyId 
                                                                                                        && x.RequiredFor <= request.EndDate 
                                                                                                        && x.RequiredFor >= request.StartDate,
                                                                                                        cancellationToken: cancellationToken);

            List<Employee> employees = await _unitOfWork.Employees.GetAllAsync(x => x.CompanyId == request.CompanyId, 
                                                                               cancellationToken: cancellationToken);

            int vehicleCount = await _unitOfWork.Vehicles.GetDbSet().Where(x => x.CompanyId == request.CompanyId 
                                                                           && x.Deleted == false)
                                                                           .CountAsync(cancellationToken: cancellationToken);

            int pendingTransportations = await _unitOfWork.Transportations.GetDbSet().Where(x => x.CompanyId == request.CompanyId 
                                                                                            && x.DriverId == null 
                                                                                            && x.Deleted == false)
                                                                                            .CountAsync(cancellationToken: cancellationToken);

            double employeeExpenses = GetEmployeeExpenses(employees);

            dashboardInfo = CreateChartData(request, dashboardInfo, transportationsForRange);
            dashboardInfo.EmployeeExpenses = employeeExpenses;
            dashboardInfo.Inflow = GetInflow(transportationsForRange);
            dashboardInfo.Outflow = GetCostsFromTransportations(transportationsForRange) + employeeExpenses;
            dashboardInfo.TransportationsInProgress = pendingTransportations;
            dashboardInfo.VehiclesCount = vehicleCount;

            return dashboardInfo;
        }

        private static DashboardInfo CreateChartData(GetDashboardInfoQuery request, DashboardInfo dashboardInfo, List<Transportation> transportationsForRange)
        {
            int daysBetweenStartAndEnd = (request.EndDate - request.StartDate).Days;

            Dictionary<DateTime, List<Transportation>> transportationsByDateForRange = PrepareData(transportationsForRange, daysBetweenStartAndEnd);

            for (int i = 0; i < daysBetweenStartAndEnd; i++)
            {
                transportationsByDateForRange.TryGetValue(request.StartDate.AddDays(i).Date, out List<Transportation>? transportationsForDay);

                if (transportationsForDay is null)
                {
                    dashboardInfo.TransportationGainsPerDay.Add(new ChartDatapoint { X = i + 1, Y = 0 });
                    dashboardInfo.TransportationCostsPerDay.Add(new ChartDatapoint { X = i + 1, Y = 0 });
                    dashboardInfo.TransportationCountPerDay.Add(new ChartDatapoint { X = i + 1, Y = 0 });
                    continue;
                }

                double transportationGains = 0;
                double transportationCosts = 0;

                foreach (Transportation transportation in transportationsForDay)
                {
                    if (transportation.Cost is not null)
                        transportationCosts += transportation.Cost.Amount;

                    transportationGains += transportation.Received.Amount;
                }

                dashboardInfo.TransportationGainsPerDay.Add(new ChartDatapoint { X = i + 1, Y = transportationGains });
                dashboardInfo.TransportationCostsPerDay.Add(new ChartDatapoint { X = i + 1, Y = transportationCosts });
                dashboardInfo.TransportationCountPerDay.Add(new ChartDatapoint { X = i + 1, Y = transportationsForDay.Count });
            }

            return dashboardInfo;
        }

        private static Dictionary<DateTime, List<Transportation>> PrepareData(List<Transportation> transportationsForRange, int daysBetweenStartAndEnd)
        {
            Dictionary<DateTime, List<Transportation>> transportationsByDateForRange = new(daysBetweenStartAndEnd);

            foreach (Transportation transportation in transportationsForRange)
            {
                if (transportationsByDateForRange.ContainsKey(transportation.RequiredFor.Date))
                    transportationsByDateForRange[transportation.RequiredFor.Date].Add(transportation);
                else
                    transportationsByDateForRange.Add(transportation.RequiredFor.Date, new List<Transportation> { transportation });
            }

            return transportationsByDateForRange;
        }

        private static double GetInflow(List<Transportation> transportationsForRange)
        {
            return transportationsForRange.Sum(x => x.Received.Amount);
        }

        private static double GetCostsFromTransportations(List<Transportation> transportationsForRange)
        {
            return transportationsForRange.Sum(x =>
            {
                if(x.Cost is null)
                    return 0;
                return x.Cost.Amount;
            });
        }

        private static double GetEmployeeExpenses(List<Employee> employees)
        {
            return employees.Sum(x => x.Salary);
        } 
    }
}

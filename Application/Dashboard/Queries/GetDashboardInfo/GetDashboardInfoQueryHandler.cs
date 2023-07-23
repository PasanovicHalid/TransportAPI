using Application.Common.Interfaces.Persistence;
using Domain.Entities;
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

            DateTime now = DateTime.Now;
            DateTime lastDayOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            List<Transportation> transportationsForMonth = await _unitOfWork.Transportations.GetAllAsync(x => x.CompanyId == request.CompanyId && x.RequiredFor <= lastDayOfMonth && x.RequiredFor >= firstDayOfMonth, cancellationToken: cancellationToken);
            List<Employee> employees = await _unitOfWork.Employees.GetAllAsync(x => x.CompanyId == request.CompanyId, cancellationToken: cancellationToken);

            int vehicleCount = await _unitOfWork.Vehicles.GetDbSet().Where(x => x.CompanyId == request.CompanyId && x.Deleted == false).CountAsync(cancellationToken: cancellationToken);
            int pendingTransportations = await _unitOfWork.Transportations.GetDbSet().Where(x => x.CompanyId == request.CompanyId && x.DriverId == null && x.Deleted == false).CountAsync(cancellationToken: cancellationToken);

            double employeeExpenses = GetEmployeeExpenses(employees);

            dashboardInfo = CreateChartData(dashboardInfo, transportationsForMonth);
            dashboardInfo.EmployeeExpenses = employeeExpenses;
            dashboardInfo.Inflow = GetInflow(transportationsForMonth);
            dashboardInfo.Outflow = GetCostsFromTransportations(transportationsForMonth) + employeeExpenses;
            dashboardInfo.TransportationsInProgress = pendingTransportations;
            dashboardInfo.VehiclesCount = vehicleCount;

            return dashboardInfo;
        }

        private static DashboardInfo CreateChartData(DashboardInfo dashboardInfo, List<Transportation> transportationsForMonth)
        {
            Dictionary<DateTime, List<Transportation>> transportationsByDateForCurrentMonth = new(DateTime.DaysInMonth(DateTime.Now.Year,
                                                                                                                                   DateTime.Now.Month));

            foreach (Transportation transportation in transportationsForMonth)
            {
                if (transportationsByDateForCurrentMonth.ContainsKey(transportation.RequiredFor.Date))
                    transportationsByDateForCurrentMonth[transportation.RequiredFor.Date].Add(transportation);
                else
                    transportationsByDateForCurrentMonth.Add(transportation.RequiredFor.Date, new List<Transportation> { transportation });
            }

            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                transportationsByDateForCurrentMonth.TryGetValue(new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1), out List<Transportation>? transportationsForDay);

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

        private static double GetInflow(List<Transportation> transportationsForMonth)
        {
            return transportationsForMonth.Sum(x => x.Received.Amount);
        }

        private static double GetCostsFromTransportations(List<Transportation> transportationsForMonth)
        {
            return transportationsForMonth.Sum(x =>
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

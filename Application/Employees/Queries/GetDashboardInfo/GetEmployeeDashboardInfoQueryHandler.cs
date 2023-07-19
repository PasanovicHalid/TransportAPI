using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Queries.GetDashboardInfo
{
    public class GetEmployeeDashboardInfoQueryHandler : IRequestHandler<GetEmployeeDashboardInfoQuery, Result<EmployeeDashboardInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeeDashboardInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EmployeeDashboardInfo>> Handle(GetEmployeeDashboardInfoQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = await _unitOfWork.Employees.GetAllAsync(x => x.CompanyId == request.CompanyId);

            EmployeeDashboardInfo result = new();
            result.TotalEmployees = employees.Count;

            foreach (var employee in employees)
            {
                if (employee is Admininistrator)
                {
                    result.TotalAdminExpenses += employee.Salary;
                    result.TotalAdmins++;
                }
                else if (employee is Driver)
                {
                    result.TotalDriverExpenses += employee.Salary;
                    result.TotalDrivers++;
                }
            }

            result.TotalEmployeeExpenses = result.TotalAdminExpenses + result.TotalDriverExpenses;

            return Result.Ok(result);
        }

    }
}

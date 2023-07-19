using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Queries.GetDashboardInfo
{
    public class GetEmployeeDashboardInfoQuery : IRequest<Result<EmployeeDashboardInfo>>
    {
        public ulong CompanyId { get; set; }
    }

    public class GetEmployeeDashboardInfoQueryValidator : AbstractValidator<GetEmployeeDashboardInfoQuery>
    {
        public GetEmployeeDashboardInfoQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

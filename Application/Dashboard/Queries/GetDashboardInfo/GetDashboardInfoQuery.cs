using Domain.PlainObjects;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.Queries.GetDashboardInfo
{
    public class GetDashboardInfoQuery : IRequest<Result<DashboardInfo>>
    {
        public ulong CompanyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class GetDashboardInfoQueryValidator : AbstractValidator<GetDashboardInfoQuery>
    {
        public GetDashboardInfoQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(y => y.StartDate).NotEmpty();
            RuleFor(z => z.EndDate).NotEmpty();
        }
    }
}

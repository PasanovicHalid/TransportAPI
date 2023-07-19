using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Queries.GetDashboardInfo
{
    public class GetTransportationDashboardInfoQuery : IRequest<Result<TransportationDashboardInfo>>
    {
        public ulong CompanyId { get; set; }
    }

    public class GetTransportationDashboardInfoQueryValidator : AbstractValidator<GetTransportationDashboardInfoQuery>
    {
        public GetTransportationDashboardInfoQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

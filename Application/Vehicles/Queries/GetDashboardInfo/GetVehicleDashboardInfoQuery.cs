using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetDashboardInfo
{
    public class GetVehicleDashboardInfoQuery : IRequest<Result<VehicleDashboardInfo>>
    {
        public ulong CompanyId { get; set; }
    }

    public class GetVehicleDashboardInfoQueryValidator : AbstractValidator<GetVehicleDashboardInfoQuery>
    {
        public GetVehicleDashboardInfoQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

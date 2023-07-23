using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetFreeVehicles
{
    public class GetFreeVehiclesByCompanyQuery : IRequest<Result<List<Vehicle>>>
    {
        public ulong CompanyId { get; set; }
    }

    public class GetFreeVehiclesByCompanyValidator : AbstractValidator<GetFreeVehiclesByCompanyQuery>
    {
        public GetFreeVehiclesByCompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

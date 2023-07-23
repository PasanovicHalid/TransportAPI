using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetAllByCompany
{
    public class GetAllVehiclesByCompanyQuery : IRequest<Result<List<Vehicle>>>
    {
        public ulong CompanyId { get; set; }
    }

    public class GetAllVehiclesByCompanyValidator : AbstractValidator<GetAllVehiclesByCompanyQuery>
    {
        public GetAllVehiclesByCompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

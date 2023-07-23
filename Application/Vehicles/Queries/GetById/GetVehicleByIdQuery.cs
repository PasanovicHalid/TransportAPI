using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles.Queries.GetById
{
    public class GetVehicleByIdQuery : IRequest<Result<Vehicle>>
    {
        public ulong VehicleId { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class GetVehicleByIdValidator : AbstractValidator<GetVehicleByIdQuery>
    {
        public GetVehicleByIdValidator()
        {
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

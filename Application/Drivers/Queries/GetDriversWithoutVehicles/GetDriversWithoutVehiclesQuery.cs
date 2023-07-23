using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Queries.GetDriversWithoutVehicles
{
    public class GetDriversWithoutVehiclesQuery : IRequest<Result<List<Driver>>>
    {
        public ulong CompanyId { get; set; }
    }

    public class GetDriversWithoutVehiclesValidator : AbstractValidator<GetDriversWithoutVehiclesQuery>
    {
        public GetDriversWithoutVehiclesValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

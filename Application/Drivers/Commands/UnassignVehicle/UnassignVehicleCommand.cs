using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Commands.UnassignVehicle
{
    public class UnassignVehicleCommand : IRequest<Result>
    {
        public ulong DriverId { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class UnassignVehicleValidator : AbstractValidator<UnassignVehicleCommand>
    {
        public UnassignVehicleValidator()
        {
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

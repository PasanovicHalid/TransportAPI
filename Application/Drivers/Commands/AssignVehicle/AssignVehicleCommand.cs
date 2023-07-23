using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Commands.AssignVehicle
{
    public class AssignVehicleCommand : IRequest<Result>
    {
        public ulong DriverId { get; set; }
        public ulong VehicleId { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class AssignVehicleValidator : AbstractValidator<AssignVehicleCommand>
    {
        public AssignVehicleValidator()
        {
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

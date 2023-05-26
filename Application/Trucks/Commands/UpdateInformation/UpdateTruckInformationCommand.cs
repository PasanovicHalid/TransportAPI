using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trucks.Commands.UpdateInformation
{
    public class UpdateTruckInformationCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
    }

    public class UpdateTruckInformationValidator : AbstractValidator<UpdateTruckInformationCommand>
    {
        public UpdateTruckInformationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.DateOfManufacturing).NotEmpty();
            RuleFor(x => x.Dimensions).NotNull();
            RuleFor(x => x.Dimensions.Width).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Dimensions.Depth).NotEmpty().GreaterThan(0);
        }
    }
}

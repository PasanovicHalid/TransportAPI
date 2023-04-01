using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Vehicles.Commands.UpdateInformation
{
    public class UpdateVehicleInformationCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class UpdateVehicleInformationValidator : AbstractValidator<UpdateVehicleInformationCommand>
    {
        public UpdateVehicleInformationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.DateOfManufacturing).NotEmpty();
            RuleFor(x => x.Dimensions).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }

}



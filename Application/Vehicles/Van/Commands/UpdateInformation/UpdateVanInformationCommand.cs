using MediatR;
using FluentResults;
using FluentValidation;
using Domain.ValueObjects;

namespace Application.Vehicles.Van.Commands.UpdateInformation
{
    public class UpdateVanInformationCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public Capacity Capacity { get; set; }
    }

    public class UpdateVanInformationValidator : AbstractValidator<UpdateVanInformationCommand>
    {
        public UpdateVanInformationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.DateOfManufacturing).NotEmpty();
            RuleFor(x => x.Dimensions).NotEmpty();
            RuleFor(x => x.Capacity).NotEmpty();
        }
    }

}



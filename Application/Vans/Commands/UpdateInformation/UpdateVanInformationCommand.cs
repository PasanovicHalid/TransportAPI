using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Vans.Commands.UpdateInformation
{
    public class UpdateVanInformationCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public Capacity Capacity { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class UpdateVanInformationValidator : AbstractValidator<UpdateVanInformationCommand>
    {
        public UpdateVanInformationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.DateOfManufacturing).NotEmpty();
            RuleFor(x => x.Dimensions).NotNull();
            RuleFor(x => x.Dimensions.Depth).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Dimensions.Width).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity).NotNull();
            RuleFor(x => x.Capacity.MaxCarryWeight).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume).NotNull();
            RuleFor(x => x.Capacity.Volume.Depth).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume.Width).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume.Height).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }

}



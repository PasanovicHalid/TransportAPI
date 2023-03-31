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
        public string AdminIdentityId { get; set; }
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
            RuleFor(x => x.AdminIdentityId).NotEmpty();
        }
    }

}



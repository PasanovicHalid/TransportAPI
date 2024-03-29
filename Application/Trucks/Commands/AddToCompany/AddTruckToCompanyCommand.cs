using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trucks.Commands.AddToCompany
{
    public class AddTruckToCompanyCommand : IRequest<Result>
    {
        public ulong CompanyId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
    }

    public class AddTruckToCompanyValidator : AbstractValidator<AddTruckToCompanyCommand>
    {
        public AddTruckToCompanyValidator()
        {
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



using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Vans.Commands.AddToCompany
{
    public class AddVanToCompanyCommand : IRequest<Result>
    {
        public ulong CompanyId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public Capacity Capacity { get; set; }
    }

    public class AddVanToCompanyValidator : AbstractValidator<AddVanToCompanyCommand>
    {
        public AddVanToCompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.DateOfManufacturing).NotEmpty();
            RuleFor(x => x.Dimensions).NotEmpty();
            RuleFor(x => x.Capacity).NotEmpty();
        }
    }

}



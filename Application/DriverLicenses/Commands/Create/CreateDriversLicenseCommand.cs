using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.DriverLicenses.Commands.Create
{
    public class CreateDriversLicenseCommand : IRequest<Result>
    {
        public string Category { get; set; }

        public DateTime IssuingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ulong DriverId { get; set; }

        public ulong CompanyId { get; set; }
    }

    public class CreateDriversLicenseValidator : AbstractValidator<CreateDriversLicenseCommand>
    {
        public CreateDriversLicenseValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.IssuingDate).NotEmpty().LessThan(x => x.ExpirationDate);
            RuleFor(x => x.ExpirationDate).NotEmpty().GreaterThan(x => x.IssuingDate);
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

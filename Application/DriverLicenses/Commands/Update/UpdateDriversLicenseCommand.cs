using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.DriverLicenses.Commands.Update
{
    public class UpdateDriversLicenseCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Category { get; set; }

        public DateTime IssuingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ulong CompanyId { get; set; }

        public ulong DriverId { get; set; }
    }

    public class UpdateDriversLicenseValidator : AbstractValidator<UpdateDriversLicenseCommand>
    {
        public UpdateDriversLicenseValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.IssuingDate).NotEmpty().LessThan(x => x.ExpirationDate);
            RuleFor(x => x.ExpirationDate).NotEmpty().GreaterThan(x => x.IssuingDate);
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.DriverId).NotEmpty();
        }
    }
}

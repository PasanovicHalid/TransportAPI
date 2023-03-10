using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Licences.DriverLicences.Commands.Create
{
    public class CreateDriversLicenceCommand : IRequest<Result>
    {
        public string Category { get; set; }

        public DateTime IssuingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ulong DriverId { get; set; }

        public string AdminIdentityId { get; set; }
    }

    public class CreateDriversLicenceValidator : AbstractValidator<CreateDriversLicenceCommand>
    {
        public CreateDriversLicenceValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.IssuingDate).NotEmpty().LessThan(x => x.ExpirationDate);
            RuleFor(x => x.ExpirationDate).NotEmpty().GreaterThan(x => x.IssuingDate);
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.AdminIdentityId).NotEmpty();
        }
    }
}

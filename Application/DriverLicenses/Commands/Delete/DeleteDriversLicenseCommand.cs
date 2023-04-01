using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.DriverLicenses.Commands.Delete
{
    public class DeleteDriversLicenseCommand : IRequest<Result>
    {
        public ulong Id { get; set; }

        public ulong CompanyId { get; set; }

        public ulong DriverId { get; set; }

        public DeleteDriversLicenseCommand()
        {
        }
    }

    public class DeleteDriversLicenseValidator : AbstractValidator<DeleteDriversLicenseCommand>
    {
        public DeleteDriversLicenseValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.DriverId).NotEmpty();
        }
    }
}

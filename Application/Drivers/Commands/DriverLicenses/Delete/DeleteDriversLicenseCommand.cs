using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Drivers.Commands.DriverLicenses.Delete
{
    public class DeleteDriversLicenseCommand : IRequest<Result>
    {
        public ulong Id { get; set; }

        public string AdminIdentityId { get; set; }

        public ulong DriverId { get; set; }

        public DeleteDriversLicenseCommand(ulong id, string adminIdentityId)
        {
            Id = id;
            AdminIdentityId = adminIdentityId;
        }

        public DeleteDriversLicenseCommand()
        {
        }
    }

    public class DeleteDriversLicenseValidator : AbstractValidator<DeleteDriversLicenseCommand>
    {
        public DeleteDriversLicenseValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.AdminIdentityId).NotEmpty();
            RuleFor(x => x.DriverId).NotEmpty();
        }
    }
}

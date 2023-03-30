using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Licences.DriverLicences.Commands.Delete
{
    public class DeleteDriversLicenseCommand : IRequest<Result>
    {
        public ulong Id { get; set; }

        public string AdminIdentityId { get; set; }

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
        }
    }
}

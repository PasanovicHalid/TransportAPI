using Application.Common.Errors;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;

namespace Application.Licences.DriverLicences.Commands.Delete
{
    public class DeleteDriversLicenseCommandHandler : IRequestHandler<DeleteDriversLicenseCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriversLicenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteDriversLicenseCommand request, CancellationToken cancellationToken)
        {
            DriversLicense? driversLicense = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role == ApplicationRolesConstants.Admin));

            if (driversLicense is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicense)));

            _unitOfWork.DriverLicenses.Remove(driversLicense);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}

using Application.Common.Errors;
using Domain.Constants;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Domain.Errors;
using Domain.Entities;

namespace Application.Licences.DriverLicences.Commands.Update
{
    internal class UpdateDriversLicenceCommandHandler : IRequestHandler<UpdateDriversLicenseCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDriversLicenceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDriversLicenseCommand request, CancellationToken cancellationToken)
        {
            DriversLicense? driversLicence = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role == ApplicationRolesConstants.Admin));

            if (driversLicence is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicense)));

            driversLicence.Category = request.Category;

            Result result = UpdateDriversLicenseInformation(request, driversLicence);

            if (result.IsFailed)
                return result;

            _unitOfWork.DriverLicenses.Update(driversLicence);
            _unitOfWork.Save();

            return Result.Ok();
        }

        private static Result UpdateDriversLicenseInformation(UpdateDriversLicenseCommand request, DriversLicense driversLicense)
        {
            Result result = driversLicense.ChangeIssuingDate(request.IssuingDate);

            if (result.IsFailed)
                return Result.Fail(new ObjectInInvalidState(nameof(DriversLicense), result.Errors));

            result = driversLicense.ChangeExpirationDate(request.ExpirationDate);

            return result.IsFailed ? Result.Fail(new ObjectInInvalidState(nameof(DriversLicense), result.Errors)) : Result.Ok();
        }
    }
}

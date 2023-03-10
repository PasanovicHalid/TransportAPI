using Application.Common.Errors;
using Application.Common.Interfaces.Persistance;
using Domain.Constants;
using Domain;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;

namespace Application.Licences.DriverLicences.Commands.Update
{
    internal class UpdateDriversLicenceCommandHandler : IRequestHandler<UpdateDriversLicenceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDriversLicenceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDriversLicenceCommand request, CancellationToken cancellationToken)
        {
            DriversLicence? driversLicence = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role == ApplicationRolesConstants.Admin));

            if (driversLicence is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicence)));

            driversLicence.Category = request.Category;
            driversLicence.ChangeIssuingDate(request.IssuingDate);
            driversLicence.ChangeExpirationDate(request.ExpirationDate);

            if(!driversLicence.IsValid())
                return Result.Fail(new ObjectInInvalidState(nameof(DriversLicence), driversLicence.ValidationResult.Errors));

            _unitOfWork.DriverLicenses.Update(driversLicence);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}

using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Drivers.Errors;
using Domain.Constants;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using MediatR;

namespace Application.DriverLicenses.Commands.Update
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
            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(d => d.Id == request.DriverId && d.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId), new List<string> { "DriversLicenses" });

            if (driver is null)
                return Result.Fail(new DriverIsntWorkingForAdminOrDoesntExist());

            DriversLicense? driversLicense = driver.DriversLicenses.Find(e => e.Id == request.Id);

            if (driversLicense is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicense)));

            Result result = UpdateDriversLicenseInformation(request, driversLicense);

            if (result.IsFailed)
                return result;

            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }

        private static Result UpdateDriversLicenseInformation(UpdateDriversLicenseCommand request, DriversLicense driversLicense)
        {
            driversLicense.Category = request.Category;

            Result result = driversLicense.ChangeIssuingDate(request.IssuingDate);

            if (result.IsFailed)
                return Result.Fail(new ObjectInInvalidState(nameof(DriversLicense), result.Errors));

            result = driversLicense.ChangeExpirationDate(request.ExpirationDate);

            return result.IsFailed ? Result.Fail(new ObjectInInvalidState(nameof(DriversLicense), result.Errors)) : Result.Ok();
        }
    }
}

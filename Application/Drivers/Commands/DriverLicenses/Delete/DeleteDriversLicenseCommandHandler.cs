using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Drivers.Errors;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Drivers.Commands.DriverLicenses.Delete
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
            Driver? driver = _unitOfWork.Drivers.GetFirstOrDefault(d => d.Id == request.DriverId && d.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId), new List<string> { "DriversLicenses" });

            if (driver is null)
                return Result.Fail(new DriverIsntWorkingForAdmin());

            DriversLicense? driversLicense = driver.DriversLicenses.Find(e => e.Id == request.Id);

            if (driversLicense is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicense)));

            driversLicense.Deleted = true;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}

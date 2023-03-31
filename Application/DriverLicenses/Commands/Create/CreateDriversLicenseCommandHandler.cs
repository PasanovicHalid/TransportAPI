using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Drivers.Errors;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.DriverLicenses.Commands.Create
{
    public class CreateDriversLicenseCommandHandler : IRequestHandler<CreateDriversLicenseCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDriversLicenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateDriversLicenseCommand request, CancellationToken cancellationToken)
        {
            Company? adminAndDriverCompany = await _unitOfWork.Companies.GetFirstOrDefaultAsync(
                c => c.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role.Equals(ApplicationRolesConstants.Admin))
                && c.Employees.Any(e => e.Id == request.DriverId && e.Role.Equals(ApplicationRolesConstants.Driver)));

            if (adminAndDriverCompany is null)
                return Result.Fail(new DriverIsntWorkingForAdminOrDoesntExist());

            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(d => d.Id == request.DriverId);

            if (driver is null)
                return Result.Fail(new EntityDoesntExist(request.DriverId, nameof(Driver)));

            DriversLicense driversLicense = new DriversLicense(request.Category,
                                                               request.ExpirationDate,
                                                               request.DriverId,
                                                               request.IssuingDate);
            driver.DriversLicenses.Add(driversLicense);

            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}

using Application.Common.Interfaces.Persistence;
using Application.Licences.DriverLicences.Errors;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Licences.DriverLicences.Commands.Create
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
            Company? adminAndDriverCompany = _unitOfWork.Companies.GetFirstOrDefault(
                c => c.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role.Equals(ApplicationRolesConstants.Admin))
                && c.Employees.Any(e => e.Id == request.DriverId && e.Role.Equals(ApplicationRolesConstants.Driver)));

            if (adminAndDriverCompany is null)
                return Result.Fail(new DriverIsntWorkingForAdmin());

            DriversLicense driversLicense = new DriversLicense(request.Category, request.ExpirationDate, request.DriverId, request.IssuingDate);

            _unitOfWork.DriverLicenses.Add(driversLicense);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}

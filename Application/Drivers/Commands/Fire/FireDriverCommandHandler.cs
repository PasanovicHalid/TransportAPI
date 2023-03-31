using Application.Common.Interfaces.Persistence;
using Application.Drivers.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Drivers.Commands.Fire
{
    public class FireDriverCommandHandler : IRequestHandler<FireDriverCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FireDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(FireDriverCommand request, CancellationToken cancellationToken)
        {
            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(e => e.Id == request.Id && e.Company!.Employees.Any(w => w.IdentityId == request.AdminIdentityId), new List<string> { "Vehicle", "DriversLicenses" }, cancellationToken: cancellationToken);

            if (driver is null)
                return Result.Fail(new DriverIsntWorkingForAdminOrDoesntExist());

            SetupDriverForDeletion(driver!);

            _unitOfWork.Drivers.Remove(driver!);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }

        private static void SetupDriverForDeletion(Driver driver)
        {
            driver!.VehicleId = null;
            driver.DriversLicenses.ForEach(e =>
            {
                e.Deleted = true;
            });
        }
    }
}




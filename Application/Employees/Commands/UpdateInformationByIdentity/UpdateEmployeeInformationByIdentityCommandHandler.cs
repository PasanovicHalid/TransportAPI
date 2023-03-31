using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Application.Employees.Errors;
using Domain.Entities;
using Application.Employees.Commands.UpdateInformationById;

namespace Application.Employees.Commands.UpdateInformationByIdentity
{
    public class UpdateEmployeeInformationByIdentityCommandHandler : IRequestHandler<UpdateEmployeeInformationByIdentityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeInformationByIdentityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateEmployeeInformationByIdentityCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(e => e.IdentityId == request.IdentityId, cancellationToken: cancellationToken);

            if (employee is null)
                return Result.Fail(new EmployeeIsntWorkingForAdminOrDoesntExist());

            SetupEmployee(request, employee);

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }

        private static void SetupEmployee(UpdateEmployeeInformationByIdentityCommand request, Employee employee)
        {
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.MiddleName = request.MiddleName;
            employee.Address = request.Address;
        }
    }
}
using Application.Common.Interfaces.Persistence;
using Application.Employees.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Employees.Commands.UpdateInformationById
{
    public class UpdateEmployeeInformationByIdCommandHandler : IRequestHandler<UpdateEmployeeInformationByIdCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeInformationByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateEmployeeInformationByIdCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(e => e.Id == request.Id && e.CompanyId == request.CompanyId,
                                                                                    cancellationToken: cancellationToken);

            if (employee is null)
                return Result.Fail(new EmployeeIsntWorkingForAdminOrDoesntExist());

            SetupEmployee(request, employee);

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }

        private static void SetupEmployee(UpdateEmployeeInformationByIdCommand request, Employee employee)
        {
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.MiddleName = request.MiddleName;
            employee.Address = request.Address;
            employee.Salary = request.Salary;
        }
    }
}

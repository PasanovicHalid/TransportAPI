using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Employees.Queries.FindById
{
    public class FindEmployeeByIdQueryHandler : IRequestHandler<FindEmployeeByIdQuery, Result<Employee>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Employee>> Handle(FindEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Employee? employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(e => e.Id == request.Id && e.Company!.Employees.Any(w => w.IdentityId == request.AdminIdentityId), cancellationToken: cancellationToken);

            if (employee is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Employee)));

            return employee;
        }
    }

}
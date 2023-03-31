using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces.Persistence;
using Application.Employees.Errors;
using Application.Common.Errors;

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
            Employee? employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(e => e.Id == request.Id && e.Company!.Employees.Any(w => w.IdentityId == request.AdminIdentityId));

            if (employee is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Employee)));

            return employee;
        }
    }

}
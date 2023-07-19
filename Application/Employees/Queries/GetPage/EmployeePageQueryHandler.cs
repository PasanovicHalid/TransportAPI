using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Employees.Queries.GetPage
{
    public class EmployeePageQueryHandler : IRequestHandler<EmployeePageQuery, Result<PaginatedList<Employee>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeePageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PaginatedList<Employee>>> Handle(EmployeePageQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<Employee> employeePage =
            await _unitOfWork.Employees.GetPageAsync(filter: x => x.CompanyId == request.CompanyId,
                                                     orderBy: request.OrderBy,
                                                     desc: request.Desc,
                                                     includeProperties: request.IncludeProperties,
                                                     withDeleted: request.WithDeleted,
                                                     tracked: request.Tracked,
                                                     pageIndex: request.PageIndex,
                                                     pageSize: request.PageSize,
                                                     cancellationToken: cancellationToken);
            return employeePage;
        }
    }
}
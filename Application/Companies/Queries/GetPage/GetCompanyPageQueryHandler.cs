using Application.Common.Queries;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Companies.Queries.GetPage
{
    public class GetCompanyPageQueryHandler : IRequestHandler<CompanyPageQuery, Result<PaginatedList<Company>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyPageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedList<Company>>> Handle(CompanyPageQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<Company> companyPage =
            await _unitOfWork.Companies.GetPageAsync(filter: request.Filter,
                                                orderBy: request.OrderBy,
                                                desc: request.Desc,
                                                includeProperties: request.IncludeProperties,
                                                withDeleted: request.WithDeleted,
                                                tracked: request.Tracked,
                                                pageIndex: request.PageIndex,
                                                pageSize: request.PageSize,
                                                cancellationToken: cancellationToken);

            return companyPage;
        }
    }
}

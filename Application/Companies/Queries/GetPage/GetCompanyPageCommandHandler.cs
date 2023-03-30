using Application.Common.Commands;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Companies.Queries.GetPage
{
    public class GetCompanyPageCommandHandler : IRequestHandler<PageCommand<Company>, Result<PaginatedList<Company>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyPageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedList<Company>>> Handle(PageCommand<Company> request, CancellationToken cancellationToken)
        {
            PaginatedList<Company> companyPage =
            await _unitOfWork.Companies.GetPage(filter: request.Filter,
                                                orderBy: request.OrderBy,
                                                desc: request.Desc,
                                                includeProperties: request.IncludeProperties,
                                                withDeleted: request.WithDeleted,
                                                tracked: request.Tracked,
                                                pageIndex: request.PageIndex,
                                                pageSize: request.PageSize);

            return companyPage;
        }
    }
    public class CompanyPageRequestValidator : PageValidator<Company> { }
}

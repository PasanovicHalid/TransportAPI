using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Transportations.Queries.GetPage
{
    public class GetTransportPageQueryHandler : IRequestHandler<GetTransportPageQuery, Result<PaginatedList<Transportation>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransportPageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedList<Transportation>>> Handle(GetTransportPageQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<Transportation> transportationPage =
            await _unitOfWork.Transportations.GetPageAsync(filter: request.Filter,
                                                  orderBy: request.OrderBy,
                                                  desc: request.Desc,
                                                  includeProperties: request.IncludeProperties,
                                                  withDeleted: request.WithDeleted,
                                                  tracked: request.Tracked,
                                                  pageIndex: request.PageIndex,
                                                  pageSize: request.PageSize,
                                                  cancellationToken: cancellationToken);
            return transportationPage;
        }
    }
}

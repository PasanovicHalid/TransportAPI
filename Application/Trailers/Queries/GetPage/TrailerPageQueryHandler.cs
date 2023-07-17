using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trailers.Queries.GetPage
{
    public class TrailerPageQueryHandler : IRequestHandler<TrailerPageQuery, Result<PaginatedList<Trailer>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrailerPageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PaginatedList<Trailer>>> Handle(TrailerPageQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<Trailer> trailerPage =
            await _unitOfWork.Trailers.GetPageAsync(filter: request.Filter,
                                                    orderBy: request.OrderBy,
                                                    desc: request.Desc,
                                                    includeProperties: request.IncludeProperties,
                                                    withDeleted: request.WithDeleted,
                                                    tracked: request.Tracked,
                                                    pageIndex: request.PageIndex,
                                                    pageSize: request.PageSize,
                                                    cancellationToken: cancellationToken);
            return trailerPage;
        }
    }
}

using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vans.Queries.GetPage
{
    public class VanPageQueryHandler : IRequestHandler<VanPageQuery, Result<PaginatedList<Van>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public VanPageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PaginatedList<Van>>> Handle(VanPageQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<Van> vanPage =
            await _unitOfWork.Vans.GetPageAsync(filter: request.Filter,
                                                orderBy: request.OrderBy,
                                                desc: request.Desc,
                                                includeProperties: request.IncludeProperties,
                                                withDeleted: request.WithDeleted,
                                                tracked: request.Tracked,
                                                pageIndex: request.PageIndex,
                                                pageSize: request.PageSize,
                                                cancellationToken: cancellationToken);
            return vanPage;
        }
    }
}

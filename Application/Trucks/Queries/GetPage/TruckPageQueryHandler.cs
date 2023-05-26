using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trucks.Queries.GetPage
{
    public class TruckPageQueryHandler : IRequestHandler<TruckPageQuery, Result<PaginatedList<Truck>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TruckPageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PaginatedList<Truck>>> Handle(TruckPageQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<Truck> truckPage =
            await _unitOfWork.Trucks.GetPageAsync(filter: request.Filter,
                                                  orderBy: request.OrderBy,
                                                  desc: request.Desc,
                                                  includeProperties: request.IncludeProperties,
                                                  withDeleted: request.WithDeleted,
                                                  tracked: request.Tracked,
                                                  pageIndex: request.PageIndex,
                                                  pageSize: request.PageSize,
                                                  cancellationToken: cancellationToken);
            return truckPage;
        }
    }
}

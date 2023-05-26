using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trucks.Queries.GetById
{
    public class GetTruckByIdQueryHandler : IRequestHandler<GetTruckByIdQuery, Result<Truck>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTruckByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Truck>> Handle(GetTruckByIdQuery request, CancellationToken cancellationToken)
        {
            Truck? truck = await _unitOfWork.Trucks.GetFirstOrDefaultAsync(t => t.Id == request.Id && t.CompanyId == request.CompanyId,
                                                                                          cancellationToken: cancellationToken, includeProperties: new List<string>() { "Trailers" });
            if (truck is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Truck)));

            return truck;
        }
    }
}

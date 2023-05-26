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

namespace Application.Trailers.Queries.GetById
{
    public class GetTrailerByIdQueryHandler : IRequestHandler<GetTrailerByIdQuery, Result<Trailer>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrailerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Trailer>> Handle(GetTrailerByIdQuery request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _unitOfWork.Trailers.GetFirstOrDefaultAsync(t => t.Id == request.Id && t.CompanyId == request.CompanyId,
                                                                                                         cancellationToken: cancellationToken, includeProperties: new List<string>() { "UsedBy" });

            if (trailer is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Trailer)));

            return trailer;
        }
    }
}

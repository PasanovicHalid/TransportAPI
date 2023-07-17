using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Vans.Queries.GetById
{
    public class GetVanByIdQueryHandler : IRequestHandler<GetVanByIdQuery, Result<Van>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetVanByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Van>> Handle(GetVanByIdQuery request, CancellationToken cancellationToken)
        {
            Van? van = await _unitOfWork.Vans.GetFirstOrDefaultAsync(v => v.Id == request.Id && v.CompanyId == request.CompanyId,
                                                                                                     cancellationToken: cancellationToken, includeProperties: new List<string>() { "Trailers" });

            if (van is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Van)));

            return van;
        }
    }
}

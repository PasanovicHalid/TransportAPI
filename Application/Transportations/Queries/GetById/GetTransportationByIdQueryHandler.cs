using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Transportations.Queries.GetById
{
    public class GetTransportationByIdQueryHandler : IRequestHandler<GetTransportationByIdQuery, Result<Transportation>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransportationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Transportation>> Handle(GetTransportationByIdQuery request, CancellationToken cancellationToken)
        {
            Transportation? transportation = await _unitOfWork.Transportations.GetFirstOrDefaultAsync(t => t.Id == request.Id);

            if (transportation is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Transportation)));

            return transportation;
        }
    }
}

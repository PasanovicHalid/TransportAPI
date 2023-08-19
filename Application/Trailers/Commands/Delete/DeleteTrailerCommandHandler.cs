using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trailers.Commands.Delete
{
    public class DeleteTrailerCommandHandler : IRequestHandler<DeleteTrailerCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTrailerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTrailerCommand request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _unitOfWork.Trailers.GetFirstOrDefaultAsync(t => t.Id == request.TrailerId && t.CompanyId == request.CompanyId,
                                                                                 cancellationToken: cancellationToken);

            if (trailer == null)
                return Result.Fail(new EntityDoesntExist(request.TrailerId, nameof(Trailer)));

            trailer.UnassignVehicle();
            _unitOfWork.Trailers.Remove(trailer);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}


using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trailers.Commands.Update
{
    public class UpdateTrailerCommandHandler : IRequestHandler<UpdateTrailerCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTrailerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTrailerCommand request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _unitOfWork.Trailers.GetFirstOrDefaultAsync(t => t.Id == request.TrailerId, cancellationToken: cancellationToken);

            if (trailer == null)
                return Result.Fail(new EntityDoesntExist(request.TrailerId, nameof(Trailer)));

            SetupTrailer(request, trailer);

            _unitOfWork.Trailers.Update(trailer);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }

        private static void SetupTrailer(UpdateTrailerCommand request, Trailer trailer)
        {
            trailer.Capacity = request.Capacity;
        }
    }
}


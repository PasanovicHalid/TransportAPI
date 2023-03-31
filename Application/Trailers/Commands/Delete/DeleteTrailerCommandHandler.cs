using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Common.Errors;

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
            Trailer? trailer = await _unitOfWork.Trailers.GetFirstOrDefaultAsync(t => t.Id == request.TrailerId, cancellationToken: cancellationToken);

            if (trailer == null)
                return Result.Fail(new EntityDoesntExist(request.TrailerId, nameof(Trailer)));

            _unitOfWork.Trailers.Remove(trailer);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}


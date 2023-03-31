using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Trailers.Commands.Delete
{
    public class DeleteTrailerCommandHandler : IRequestHandler<DeleteTrailerCommand, Result>
    {
        public async Task<Result> Handle(DeleteTrailerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


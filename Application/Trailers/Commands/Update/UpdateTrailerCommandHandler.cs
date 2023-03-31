using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Trailers.Commands.Update
{
    public class UpdateTrailerCommandHandler : IRequestHandler<UpdateTrailerCommand, Result>
    {
        public async Task<Result> Handle(UpdateTrailerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


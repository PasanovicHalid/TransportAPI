using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Trailers.Commands.Create
{
    public class CreateTrailerForCompanyCommandHandler : IRequestHandler<CreateTrailerForCompanyCommand, Result>
    {
        public async Task<Result> Handle(CreateTrailerForCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


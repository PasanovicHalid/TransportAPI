using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Vehicles.Van.Commands.AddVanToCompany
{
    public class AddVanToCompanyCommandHandler : IRequestHandler<AddVanToCompanyCommand, Result>
    {
        public async Task<Result> Handle(AddVanToCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


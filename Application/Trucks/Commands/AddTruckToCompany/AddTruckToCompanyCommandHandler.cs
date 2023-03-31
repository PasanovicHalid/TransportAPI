using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Vehicles.Truck.Commands.AddTruckToCompany
{
    public class AddTruckToCompanyCommandHandler : IRequestHandler<AddTruckToCompanyCommand, Result>
    {
        public async Task<Result> Handle(AddTruckToCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


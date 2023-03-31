using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Vehicles.Van.Commands.UpdateInformation
{
    public class UpdateVanInformationCommandHandler : IRequestHandler<UpdateVanInformationCommand, Result>
    {
        public async Task<Result> Handle(UpdateVanInformationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


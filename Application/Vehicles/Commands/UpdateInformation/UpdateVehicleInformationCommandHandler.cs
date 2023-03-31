using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Vehicles.Commands.UpdateInformation
{
    public class UpdateVehicleInformationCommandHandler : IRequestHandler<UpdateVehicleInformationCommand, Result>
    {
        public async Task<Result> Handle(UpdateVehicleInformationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


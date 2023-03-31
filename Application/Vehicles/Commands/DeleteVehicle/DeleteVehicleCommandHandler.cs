using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
    {
        public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Trailers.Commands.AddToVehicle
{
    public class AddTrailerToVehicleCommandHandler : IRequestHandler<AddTrailerToVehicleCommand, Result>
    {
        public async Task<Result> Handle(AddTrailerToVehicleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
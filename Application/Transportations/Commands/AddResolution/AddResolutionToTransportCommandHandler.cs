using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Transportations.Commands.AddResolution
{
    public class AddResolutionToTransportCommandHandler : IRequestHandler<AddResolutionToTransportationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddResolutionToTransportCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddResolutionToTransportationCommand request, CancellationToken cancellationToken)
        {
            Transportation? transport = await _unitOfWork.Transportations.GetFirstOrDefaultAsync(t => t.Id == request.TransportationId && t.CompanyId == request.CompanyId);

            if (transport is null)
                return Result.Fail(new EntityDoesntExist(request.TransportationId, nameof(Transportation)));

            Driver? driver = await _unitOfWork.Drivers.GetFirstOrDefaultAsync(d => d.Id == request.DriverId && d.CompanyId == transport.CompanyId);

            if (driver is null)
                return Result.Fail(new EntityDoesntExist(request.DriverId, nameof(Driver)));

            Result result = transport.AddResolution(request.Cost, request.DriverId, request.StartLocation);

            if (result.IsFailed)
                return result;

            _unitOfWork.Transportations.Update(transport);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}

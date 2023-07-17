using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trucks.Commands.UpdateInformation
{
    public class UpdateTruckInformationCommandHandler : IRequestHandler<UpdateTruckInformationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTruckInformationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTruckInformationCommand request, CancellationToken cancellationToken)
        {
            Truck? truck = await _unitOfWork.Trucks.GetFirstOrDefaultAsync(t => t.Id == request.Id && t.CompanyId == request.CompanyId,
                                                                                          cancellationToken: cancellationToken);

            if (truck is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Truck)));

            truck.UpdateInformation(request.Manufacturer, request.Model, request.DateOfManufacturing, request.Dimensions);

            _unitOfWork.Trucks.Update(truck);

            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

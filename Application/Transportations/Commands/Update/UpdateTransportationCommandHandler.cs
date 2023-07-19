using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Transportations.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Commands.Update
{
    public class UpdateTransportationCommandHandler : IRequestHandler<UpdateTransportationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransportationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTransportationCommand request, CancellationToken cancellationToken)
        {
            Transportation? transportation = await _unitOfWork.Transportations.GetFirstOrDefaultAsync(t => t.Id == request.TransportationId);

            if (transportation is null)
                return Result.Fail(new EntityDoesntExist(request.TransportationId, nameof(Transportation)));

            if(transportation.CompanyId != request.CompanyId)
                return Result.Fail(new UpdatingTransportOfOtherCompanyNotAllowed());

            Result result = transportation.UpdateInformation(request.DateOfDeparture,
                                             request.DateOfArrival,
                                             request.Cargo,
                                             request.Destination,
                                             request.Received);

            if (result.IsFailed)
                return Result.Fail(new UpdatingTransportFailed());

            _unitOfWork.Transportations.Update(transportation);

            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}

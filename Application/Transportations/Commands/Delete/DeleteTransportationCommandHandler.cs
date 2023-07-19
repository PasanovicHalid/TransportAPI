using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Commands.Delete
{
    public class DeleteTransportationCommandHandler : IRequestHandler<DeleteTransportationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransportationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTransportationCommand request, CancellationToken cancellationToken)
        {
            Transportation? transportation = await _unitOfWork.Transportations.GetFirstOrDefaultAsync(t => t.Id == request.TransportationId && t.CompanyId == request.CompanyId,
                                                                                                      cancellationToken: cancellationToken);

            if (transportation is null)
                return Result.Fail(new EntityDoesntExist(request.TransportationId, nameof(Transportation)));

            _unitOfWork.Transportations.Remove(transportation);

            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

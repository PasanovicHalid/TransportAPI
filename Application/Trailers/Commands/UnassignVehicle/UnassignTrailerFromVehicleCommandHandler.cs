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

namespace Application.Trailers.Commands.UnassignVehicle
{
    public class UnassignTrailerFromVehicleCommandHandler : IRequestHandler<UnassignTrailerFromVehicleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnassignTrailerFromVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UnassignTrailerFromVehicleCommand request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _unitOfWork.Trailers.GetFirstOrDefaultAsync(x => x.Id == request.TrailerId && x.CompanyId == request.CompanyId,
                                                                                 cancellationToken: cancellationToken);
            if (trailer is null)
                return Result.Fail(new EntityDoesntExist(request.TrailerId, nameof(Trailer)));

            Result result = trailer.UnassignVehicle();

            if (result.IsFailed)
                return result;

            _unitOfWork.Trailers.Update(trailer);

            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}

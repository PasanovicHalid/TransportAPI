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

namespace Application.Vans.Commands.Delete
{
    public class DeleteVanCommandHandler : IRequestHandler<DeleteVanCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteVanCommand request, CancellationToken cancellationToken)
        {
            Van? van = await _unitOfWork.Vans.GetFirstOrDefaultAsync(v => v.Id == request.Id && v.CompanyId == request.CompanyId, cancellationToken: cancellationToken, includeProperties: new List<string> { "Trailers" });

            if (van is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Van)));

            foreach (Trailer trailer in van.Trailers)
                trailer.VehicleId = null;

            van.DriverId = null;

            _unitOfWork.Vans.Remove(van);

            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

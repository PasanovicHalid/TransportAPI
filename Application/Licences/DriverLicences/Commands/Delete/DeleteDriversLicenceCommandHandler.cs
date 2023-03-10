using Application.Common.Errors;
using Application.Common.Interfaces.Persistance;
using Domain;
using Domain.Constants;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Licences.DriverLicences.Commands.Delete
{
    public class DeleteDriversLicenceCommandHandler : IRequestHandler<DeleteDriversLicenceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriversLicenceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteDriversLicenceCommand request, CancellationToken cancellationToken)
        {
            DriversLicence? driversLicence = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role == ApplicationRolesConstants.Admin));

            if (driversLicence is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicence)));

            _unitOfWork.DriverLicenses.Remove(driversLicence);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}

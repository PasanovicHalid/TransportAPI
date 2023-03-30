using Application.Common.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;

namespace Application.Licences.DriverLicences.Queries.FindByIdByDriver
{
    public class FindDriversLicenseByIdByDriverCommandHandler : IRequestHandler<FindDriversLicenseByIdByDriverCommand, Result<DriversLicense>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindDriversLicenseByIdByDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DriversLicense>> Handle(FindDriversLicenseByIdByDriverCommand request, CancellationToken cancellationToken)
        {
            DriversLicense? driversLicence = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.IdentityId == request.DriverIdentityId);

            if (driversLicence is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicense)));

            return driversLicence;
        }
    }
}

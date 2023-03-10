using Application.Common.Errors;
using Application.Common.Interfaces.Persistance;
using Domain;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Licences.DriverLicences.Queries.FindByIdByDriver
{
    public class FindDriversLicenceByIdByDriverCommandHandler : IRequestHandler<FindDriversLicenceByIdByDriverCommand, Result<DriversLicence>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindDriversLicenceByIdByDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DriversLicence>> Handle(FindDriversLicenceByIdByDriverCommand request, CancellationToken cancellationToken)
        {
            DriversLicence? driversLicence = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.IdentityId == request.DriverIdentityId);

            if (driversLicence is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicence)));

            return driversLicence;
        }
    }
}

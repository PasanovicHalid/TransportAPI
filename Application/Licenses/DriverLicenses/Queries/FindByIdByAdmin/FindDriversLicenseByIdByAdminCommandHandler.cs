using Application.Common.Errors;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;

namespace Application.Licences.DriverLicences.Queries.FindByIdByAdmin
{
    public class FindDriversLicenseByIdByAdminCommandHandler : IRequestHandler<FindDriversLicenseByIdByAdminCommand, Result<DriversLicense>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindDriversLicenseByIdByAdminCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DriversLicense>> Handle(FindDriversLicenseByIdByAdminCommand request, CancellationToken cancellationToken)
        {
            DriversLicense? driversLicence = _unitOfWork.DriverLicenses.GetFirstOrDefault(d => d.Id == request.Id && d.Driver!.Company!.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role == ApplicationRolesConstants.Admin));

            if (driversLicence is null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(DriversLicense)));

            return driversLicence;
        }
    }
}

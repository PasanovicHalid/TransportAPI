using Application.Common.Errors;
using Application.Common.Interfaces.Persistance;
using Application.Licences.DriverLicences.Errors;
using Domain;
using Domain.Companies;
using Domain.Constants;
using Domain.Employees;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Licences.DriverLicences.Commands.Create
{
    public class CreateDriversLicenceCommandHandler : IRequestHandler<CreateDriversLicenceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDriversLicenceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateDriversLicenceCommand request, CancellationToken cancellationToken)
        {
            Company? adminAndDriverCompany = _unitOfWork.Companies.GetFirstOrDefault(
                c => c.Employees.Any(e => e.IdentityId == request.AdminIdentityId && e.Role.Equals(ApplicationRolesConstants.Admin)) 
                && c.Employees.Any(e => e.Id == request.DriverId && e.Role.Equals(ApplicationRolesConstants.Driver)));

            if (adminAndDriverCompany is null)
                return Result.Fail(new DriverIsntWorkingForAdmin());

            DriversLicence driversLicence = new DriversLicence(request.Category, request.ExpirationDate, request.DriverId, request.IssuingDate);

            _unitOfWork.DriverLicenses.Add(driversLicence);
            _unitOfWork.Save();

            return Result.Ok();            
        }
    }
}

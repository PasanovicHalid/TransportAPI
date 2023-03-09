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
            Employee? admin = _unitOfWork.Employees.GetFirstOrDefault(e => e.IdentityId == request.AdminIdentityId);

            if (admin is null)
                return Result.Fail(new AdminDoesntExist());

            Employee? driver = _unitOfWork.Employees.GetFirstOrDefault(e => e.Id == request.DriverId);

            if(driver is null)
                return Result.Fail(new EntityDoesntExist(request.DriverId, "Driver", HttpStatusCode.BadRequest));

            if (!driver.Role.Equals(ApplicationRolesConstants.Driver))
                return Result.Fail(new EmployeeIsntDriver());

            Company? adminAndDriverCompany = _unitOfWork.Companies.GetFirstOrDefault(
                c => c.Employees.Select(e => e.IdentityId == admin.IdentityId).Count() == 1 
                && c.Employees.Select(e => e.Id == request.DriverId).Count() == 1);

            if (adminAndDriverCompany is null)
                return Result.Fail(new DriverIsntWorkingForAdmin());

            DriversLicence driversLicence = new DriversLicence(request.Category, request.ExpirationDate, request.DriverId, request.IssuingDate);

            _unitOfWork.DriverLicenses.Add(driversLicence);
            _unitOfWork.Save();

            return Result.Ok();            
        }
    }
}

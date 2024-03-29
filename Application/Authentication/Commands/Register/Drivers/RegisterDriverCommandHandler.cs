﻿using Application.Authentication.Commands.Register.Errors;
using Application.Authentication.Contracts;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Register.Drivers
{
    public class RegisterDriverCommandHandler : IRequestHandler<RegisterDriverCommand, Result<AuthenticationResult>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterDriverCommandHandler(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthenticationResult>> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
        {
            Company? adminCompany = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.CompanyId);

            if (adminCompany is null)
                return Result.Fail(new AdminCompanyDoesntExist());

            if (await _userManager.FindByNameAsync(request.Email) != null)
                return Result.Fail(new UserWithSameEmailExists());

            Driver driver = SetupDriver(request, adminCompany);

            using var transtaction = _unitOfWork.BeginTransaction();
            try
            {
                IdentityResult result = await _userManager.CreateAsync(driver.User!, request.Password);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while creating user"));

                result = await _userManager.AddToRoleAsync(driver.User!, ApplicationRolesConstants.Driver);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while assigning role"));

                adminCompany.Employees.Add(driver);

                _unitOfWork.Companies.Update(adminCompany);
                await _unitOfWork.SaveAsync(cancellationToken);

                transtaction.Commit();

                return new AuthenticationResult
                {
                    Token = await _jwtGenerator.GenerateTokenAsync(driver.User!, adminCompany.Id),
                    ExpirationDate = _jwtGenerator.GetExpirationDate()
                };
            }
            catch
            {
                transtaction.Rollback();
                throw;
            }
        }

        private static Driver SetupDriver(RegisterDriverCommand request, Company company)
        {
            if (request.Address.GpsCoordinate is not null)
            {
                return new(user: new IdentityUser()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                },
                                firstName: request.FirstName,
                                middleName: request.MiddleName,
                                lastName: request.LastName,
                                salary: request.Salary,
                                address: new Address(street: request.Address.Street,
                                                     city: request.Address.City,
                                                     state: request.Address.State,
                                                     postalCode: request.Address.PostalCode,
                                                     country: request.Address.Country,
                                                     gpsCoordinate: new GpsCoordinate(request.Address.GpsCoordinate.Longitude, request.Address.GpsCoordinate.Latitude)),
                                companyId: request.CompanyId);
            }
            else
            {
                return new(user: new IdentityUser()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                },
                                firstName: request.FirstName,
                                middleName: request.MiddleName,
                                lastName: request.LastName,
                                salary: request.Salary,
                                address: new Address(street: request.Address.Street,
                                                     city: request.Address.City,
                                                     state: request.Address.State,
                                                     postalCode: request.Address.PostalCode,
                                                     country: request.Address.Country),
                                companyId: request.CompanyId);
            }
        }
    }
}

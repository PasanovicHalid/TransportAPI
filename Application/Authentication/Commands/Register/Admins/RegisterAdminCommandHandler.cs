using Application.Authentication.Commands.Register.Errors;
using Application.Authentication.Contracts;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Register.Admins
{
    public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, Result<AuthenticationResult>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterAdminCommandHandler(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthenticationResult>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.CompanyId);

            if (company is null)
                return Result.Fail(new EntityDoesntExist(request.CompanyId, nameof(Company)));

            if (await _userManager.FindByNameAsync(request.Email) != null)
                return Result.Fail(new UserWithSameEmailExists());

            Admininistrator admin = SetupAdmin(request);

            using var transtaction = _unitOfWork.BeginTransaction();
            try
            {
                IdentityResult result = await _userManager.CreateAsync(admin.User!, request.Password);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while creating user"));

                result = await _userManager.AddToRoleAsync(admin.User!, ApplicationRolesConstants.Admin);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while assigning role"));

                company.Employees.Add(admin);

                _unitOfWork.Companies.Update(company);
                await _unitOfWork.SaveAsync(cancellationToken);

                transtaction.Commit();

                return new AuthenticationResult
                {
                    Token = await _jwtGenerator.GenerateTokenAsync(admin.User!, company.Id),
                    ExpirationDate = _jwtGenerator.GetExpirationDate()
                };
            }
            catch
            {
                transtaction.Rollback();
                throw;
            }
        }

        private static Admininistrator SetupAdmin(RegisterAdminCommand request)
        {
            if (request.Address.GpsCoordinate is not null)
            {
                return new(user: new IdentityUser()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                },
                                role: ApplicationRolesConstants.Admin,
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
                                role: ApplicationRolesConstants.Admin,
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

using Application.Authentication.Commands.Register.Errors;
using Application.Authentication.Contracts;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain;
using Domain.Companies;
using Domain.Constants;
using Domain.Employees;
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
            if (await _userManager.FindByNameAsync(request.Email) != null)
                return Result.Fail(new UserWithSameEmailExists());

            Company? adminCompany = _unitOfWork.Companies.GetFirstOrDefault(c => c.Employees.Any(e => e.IdentityId == request.AdminIdentityId), tracked: false);

            if(adminCompany is null)
                return Result.Fail(new AdminCompanyDoesntExist());

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

                _unitOfWork.Drivers.Add(driver);
                _unitOfWork.Save();

                transtaction.Commit();

                return new AuthenticationResult
                {
                    Token = await _jwtGenerator.GenerateTokenAsync(driver.User!),
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
            return new Driver(new IdentityUser()
                              {
                                  UserName = request.Email,
                                  Email = request.Email,
                                  PhoneNumber = request.PhoneNumber
                              },
                              request.FirstName,
                              request.MiddleName,
                              request.LastName,
                              request.Salary,
                              new Address(request.Street,
                                          request.City,
                                          request.State,
                                          request.PostalCode,
                                          request.Country),
                              company.Id);
        }
    }
}

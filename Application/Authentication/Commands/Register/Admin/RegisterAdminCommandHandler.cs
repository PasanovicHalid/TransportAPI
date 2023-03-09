using Application.Authentication.Commands.Register.Errors;
using Application.Authentication.Commands.Register.SuperAdmin;
using Application.Authentication.Contracts;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain;
using Domain.Constants;
using Domain.Employees;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register.Admin
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
            if (await _userManager.FindByNameAsync(request.Email) != null)
                return Result.Fail(new UserWithSameEmailExists());

            Employee admin = SetupAdmin(request);

            using var transtaction = _unitOfWork.BeginTransaction();

            try
            {
                IdentityResult result = await _userManager.CreateAsync(admin.User!, request.Password);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while creating user"));

                result = await _userManager.AddToRoleAsync(admin.User!, ApplicationRolesConstants.Admin);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while assigning role"));

                _unitOfWork.Employees.Add(admin);
                _unitOfWork.Save();

                transtaction.Commit();

                return new AuthenticationResult
                {
                    Token = await _jwtGenerator.GenerateTokenAsync(admin.User!),
                    ExpirationDate = _jwtGenerator.GetExpirationDate()
                };
            }
            catch
            {
                transtaction.Rollback();
                throw;
            }
        }

        private static Employee SetupAdmin(RegisterAdminCommand request)
        {
            return new Employee(
                                user: new IdentityUser() 
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
                                address: new Address(street: request.Street,
                                                     city: request.City,
                                                     state: request.State,
                                                     postalCode: request.PostalCode,
                                                     country: request.Country),
                                companyId: request.CompanyId);
        }
    }
}

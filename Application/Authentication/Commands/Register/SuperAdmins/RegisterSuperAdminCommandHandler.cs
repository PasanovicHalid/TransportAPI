using Application.Authentication.Commands.Register.Errors;
using Application.Authentication.Contracts;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Register.SuperAdmins
{
    public class RegisterSuperAdminCommandHandler : IRequestHandler<RegisterSuperAdminCommand, Result<AuthenticationResult>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterSuperAdminCommandHandler(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthenticationResult>> Handle(RegisterSuperAdminCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByNameAsync(request.Email) != null)
                return Result.Fail(new UserWithSameEmailExists());

            IdentityUser user = SetupUser(request);

            using var transtaction = _unitOfWork.BeginTransaction();

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while creating user"));

                result = await _userManager.AddToRoleAsync(user, ApplicationRolesConstants.SuperAdmin);

                if (!result.Succeeded)
                    return Result.Fail(new Error("Something failed while assigning role"));

                transtaction.Commit();

                return new AuthenticationResult
                {
                    Token = await _jwtGenerator.GenerateTokenAsync(user, 0),
                    ExpirationDate = _jwtGenerator.GetExpirationDate()
                };
            }
            catch
            {
                transtaction.Rollback();
                throw;
            }
        }

        private static IdentityUser SetupUser(RegisterSuperAdminCommand request)
        {
            return new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
        }
    }
}

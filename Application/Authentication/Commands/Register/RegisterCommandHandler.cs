using Application.Authentication.Commands.Register.Exceptions;
using Application.Authentication.Contracts;
using Application.Common.Interfaces.Authentication;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public RegisterCommandHandler(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtGenerator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Result<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return Result.Fail(new UserWithSameEmailExists());

            IdentityUser user = SetupUser(request);

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Result.Fail(new Error("Something failed while creating user"));

            result = await _userManager.AddToRoleAsync(user!, request.UserType);

            if (!result.Succeeded)
                return Result.Fail(new Error("Something failed while assigning role"));

            return new AuthenticationResult
            {
                Token = await _jwtGenerator.GenerateTokenAsync(user),
                ExpirationDate = _jwtGenerator.GetExpirationDate()
            };
        }

        private static IdentityUser SetupUser(RegisterCommand request)
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

using Application.Authentication.Contracts;
using Application.Authentication.Queries.Login.Errors;
using Application.Common.Interfaces.Authentication;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public LoginQueryHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(request.Email);

            if(user == null)
                return Result.Fail(new LoginFailed());

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
                return Result.Fail(new LoginFailed());

            return new AuthenticationResult
            {
                Token = await _jwtGenerator.GenerateTokenAsync(user),
                ExpirationDate = _jwtGenerator.GetExpirationDate()
            };
        }
    }
}

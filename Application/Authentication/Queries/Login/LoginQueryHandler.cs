using Application.Authentication.Contracts;
using Application.Authentication.Queries.Login.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public LoginQueryHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
                return Result.Fail(new LoginFailed());

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
                return Result.Fail(new LoginFailed());


            Employee? employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(e => e.IdentityId == user.Id);

            string token = employee is null
                ? await _jwtGenerator.GenerateTokenAsync(user, 0)
                : await _jwtGenerator.GenerateTokenAsync(user, employee.CompanyId);

            return new AuthenticationResult
            {
                Token = token,
                ExpirationDate = _jwtGenerator.GetExpirationDate()
            };
        }
    }
}

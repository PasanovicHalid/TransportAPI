using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary.Users.Exceptions;
using TransportLibrary.Authentication.Exceptions;
using TransportLibrary.Authentication.Services.Interfaces;

namespace TransportLibrary.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtGeneratorService _jwtService;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtGeneratorService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                throw new ApplicationUserDoesntExistException("User with username: " + username + " doesn't exist");
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if(!result.Succeeded) 
            {
                throw new FailedLoginException("Failed Login! Invalid Username or Password");
            }

            return _jwtService.GenerateToken(user);
        }

        public async Task<string> RegisterDriverAsync(ApplicationUser user, string password)
        {
            if(await _userManager.FindByEmailAsync(user.Email) != null)
            {
                throw new ApplicationUserWithSameEmailExistsException("User with same email exists already");
            }

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception("Internal error");

            user = await _userManager.FindByEmailAsync(user.Email);

            result = await _userManager.AddToRoleAsync(user, ApplicationRolesConstants.Driver);

            if (!result.Succeeded)
                throw new Exception("Internal error");

            return _jwtService.GenerateToken(user);
        }
    }
}

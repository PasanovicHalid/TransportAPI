using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransportAPI.Authentication.DTOs;
using TransportLibrary.Authentication;
using TransportLibrary.Authentication.Exceptions;
using TransportLibrary.Authentication.Services.Interfaces;

namespace TransportAPI.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginInfo)
        {
            return Ok(await _authService.LoginAsync(loginInfo.Username, loginInfo.Password));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterDriver([FromBody] DriverRegistrationDTO registrationInfo)
        {
            ApplicationUser user = SetupUser(registrationInfo);

            string token = await _authService.RegisterDriverAsync(user, registrationInfo.Password);

            return CreatedAtAction(nameof(RegisterDriver), new DriverRegisterResultDTO
            {
                Succeeded = true,
                Token = new TokenDTO()
                {
                    Token = token
                }
            });
        }

        private static ApplicationUser SetupUser(DriverRegistrationDTO registrationInfo)
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = registrationInfo.Email;
            user.UserName = registrationInfo.Username;
            user.PhoneNumber = registrationInfo.PhoneNumber;
            return user;
        }
    }
}

using Application.Common.Interfaces.Authentication;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Common.Controllers;

namespace Presentation.Controllers
{
    [Authorize]
    public class WeatherForecastController : ApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJwtTokenGenerator jwtTokenGenerator, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        [HttpGet("TestNoLogin")]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> GetNoLogin()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("TestJwt")]
        [AllowAnonymous]
        public async Task<string> GetNo()
        {
            var user = await _userManager.FindByEmailAsync("admin@gmail.com");
            return await _jwtTokenGenerator.GenerateTokenAsync(user);
        }

        [HttpGet("TestException")] 
        [AllowAnonymous]
        public async Task<string> GetException()
        {
            throw new Exception("Test");
        }

        [HttpGet("TestAdmin")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("TestDriver")]
        [Authorize(Roles = ApplicationRolesConstants.Driver)]
        public IEnumerable<WeatherForecast> Test()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }

        [HttpGet("TestDriverAdmin")]
        [Authorize(Roles = ApplicationRolesConstants.Driver + "," + ApplicationRolesConstants.Admin)]
        public IEnumerable<WeatherForecast> Test1()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }
    }
}
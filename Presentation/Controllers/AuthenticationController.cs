using Application.Authentication.Commands.Register.Admins;
using Application.Authentication.Commands.Register.Drivers;
using Application.Authentication.Commands.Register.SuperAdmins;
using Application.Authentication.Contracts;
using Application.Authentication.Queries.Login;
using AutoMapper;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Authentication;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginQuery query = _mapper.Map<LoginQuery>(request);

            Result<AuthenticationResult> result = await _mediator.Send(query);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok(_mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register/superAdmin")]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] RegistrationRequest request)
        {
            RegisterSuperAdminCommand command = _mapper.Map<RegisterSuperAdminCommand>(request);

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterSuperAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register/initial/admin")]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> RegisterInitialAdminForCompany([FromBody] AdminRegistrationRequest request)
        {
            RegisterAdminCommand command = _mapper.Map<RegisterAdminCommand>(request);

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register/admin")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegistrationRequest request)
        {
            RegisterAdminCommand command = _mapper.Map<RegisterAdminCommand>(request);

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register/driver")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> RegisterDriver([FromBody] DriverRegistrationRequest request)
        {
            RegisterDriverCommand command = _mapper.Map<RegisterDriverCommand>(request);
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }
    }
}

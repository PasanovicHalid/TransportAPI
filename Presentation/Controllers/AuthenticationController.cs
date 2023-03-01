using Application.Authentication.Commands.Register;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Authentication;
using Application.Authentication.Contracts;
using Application.Authentication.Queries.Login;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Authentication.Commands.Register.Exceptions;
using Constants;

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
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            LoginQuery query = _mapper.Map<LoginQuery>(request);

            Result<AuthenticationResult>? result = (Result<AuthenticationResult>?) await _mediator.Send(query);

            if (result is null)
                return Problem();

            if (result!.IsFailed)
            {
                IError error = result.Errors[0];
                return Problem(statusCode: (int) HttpStatusCode.NotFound, title: error.Message);
            }

            return Ok(_mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterDriver([FromBody] RegistrationRequest request)
        {
            RegisterCommand command = _mapper.Map<RegisterCommand>(request);
            command.UserType = ApplicationRolesConstants.Driver;

            Result<AuthenticationResult>? result = (Result<AuthenticationResult>?) await _mediator.Send(command);

            if (result is null)
                return Problem();

            if (result.IsFailed)
                return HandleRegistrationErrors(result.Errors);

            return CreatedAtAction(nameof(RegisterDriver), _mapper.Map<AutheticationResponse>(result.Value));
        }

        private IActionResult HandleRegistrationErrors(List<IError> errors)
        {
            IError error = errors[0];

            if (error is UserWithSameEmailExists)
                return Problem(statusCode: (int)HttpStatusCode.Forbidden, title: error.Message);

            return Problem(statusCode: (int)HttpStatusCode.InternalServerError, title: error.Message);
        }
    }
}

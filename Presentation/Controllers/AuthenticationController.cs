﻿using Application.Authentication.Commands.Register;
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
using Application.Common.Behaviors;
using Domain.Constants;

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

            Result<AuthenticationResult>? result = (Result<AuthenticationResult>?)await _mediator.Send(query);

            return SetupLoginResponse(result);
        }

        [HttpPost("register/driver")]
        public async Task<IActionResult> RegisterDriver([FromBody] RegistrationRequest request)
        {
            RegisterCommand command = SetupRegisterDriverCommand(request);

            Result<AuthenticationResult>? result = (Result<AuthenticationResult>?)await _mediator.Send(command);

            return SetupRegisterDriverResponse(result);
        }

        private IActionResult SetupRegisterDriverResponse(Result<AuthenticationResult>? result)
        {
            if (result is null)
                return Problem();

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterDriver), _mapper.Map<AutheticationResponse>(result.Value));
        }

        private RegisterCommand SetupRegisterDriverCommand(RegistrationRequest request)
        {
            RegisterCommand command = _mapper.Map<RegisterCommand>(request);
            command.UserType = ApplicationRolesConstants.Driver;
            return command;
        }

        private IActionResult SetupLoginResponse(Result<AuthenticationResult>? result)
        {
            if (result is null)
                return Problem();

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok(_mapper.Map<AutheticationResponse>(result.Value));
        }
    }
}
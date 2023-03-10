using Application.Licences.DriverLicences.Commands.Create;
using Application.Licences.DriverLicences.Commands.Delete;
using Application.Licences.DriverLicences.Commands.Update;
using Application.Licences.DriverLicences.Queries.FindByIdByAdmin;
using Application.Licences.DriverLicences.Queries.FindByIdByDriver;
using AutoMapper;
using Domain;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Licences.DriversLicences;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Presentation.Controllers.Licences
{
    [Authorize]
    public class DriversLicenceController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DriversLicenceController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateDriversLicenceRequest request)
        {
            CreateDriversLicenceCommand command = _mapper.Map<CreateDriversLicenceCommand>(request);
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return CreatedAtAction(nameof(Create), null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Delete([FromRoute] ulong id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            DeleteDriversLicenceCommand command = new DeleteDriversLicenceCommand(id, userId);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Update([FromBody] UpdateDriversLicenceRequest request)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            UpdateDriversLicenceCommand command = _mapper.Map<UpdateDriversLicenceCommand>(request);
            command.AdminIdentityId = userId;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Driver)]
        public async Task<IActionResult> GetById([FromRoute] ulong id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            FindDriversLicenceByIdByDriverCommand command = new FindDriversLicenceByIdByDriverCommand
            {
                Id = id,
                DriverIdentityId = userId
            };

            Result<DriversLicence> response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }

        [HttpGet("admin/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin([FromRoute] ulong id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            FindDriversLicenceByIdByAdminCommand command = new FindDriversLicenceByIdByAdminCommand
            {
                Id = id,
                AdminIdentityId = userId
            };

            Result<DriversLicence> response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }
    }
}

using Application.Licences.DriverLicences.Commands.Create;
using Application.Licences.DriverLicences.Commands.Delete;
using Application.Licences.DriverLicences.Commands.Update;
using Application.Licences.DriverLicences.Queries.FindByIdByAdmin;
using Application.Licences.DriverLicences.Queries.FindByIdByDriver;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
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
            CreateDriversLicenseCommand command = _mapper.Map<CreateDriversLicenseCommand>(request);
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
            DeleteDriversLicenseCommand command = new DeleteDriversLicenseCommand(id, userId);

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
            UpdateDriversLicenseCommand command = _mapper.Map<UpdateDriversLicenseCommand>(request);
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
            FindDriversLicenseByIdByDriverCommand command = new FindDriversLicenseByIdByDriverCommand
            {
                Id = id,
                DriverIdentityId = userId
            };

            Result<DriversLicense> response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }

        [HttpGet("admin/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin([FromRoute] ulong id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            FindDriversLicenseByIdByAdminCommand command = new FindDriversLicenseByIdByAdminCommand
            {
                Id = id,
                AdminIdentityId = userId
            };

            Result<DriversLicense> response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }
    }
}

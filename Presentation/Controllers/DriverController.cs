using Application.Drivers.Commands.DriverLicenses.Create;
using Application.Drivers.Commands.DriverLicenses.Delete;
using Application.Drivers.Commands.DriverLicenses.Update;
using AutoMapper;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Licences.DriversLicences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class DriverController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DriverController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{id}/licenses")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> AddLicense([FromBody] CreateDriversLicenceRequest request, [FromRoute(Name = "id")] ulong driverId)
        {
            CreateDriversLicenseCommand command = _mapper.Map<CreateDriversLicenseCommand>(request);
            command.DriverId = driverId;
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return CreatedAtAction(nameof(AddLicense), null);
        }

        [HttpDelete("{id}/licenses/{licenseId}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> DeleteLicense([FromRoute(Name = "licenseId")] ulong licenseId, [FromRoute(Name = "id")] ulong driverId)
        {
            DeleteDriversLicenseCommand command = new()
            {
                DriverId = driverId,
                Id = licenseId,
                AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
            };

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPut("{id}/licenses/{licenseId}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UpdateLicense([FromBody] UpdateDriversLicenceRequest request, [FromRoute(Name = "licenseId")] ulong licenseId, [FromRoute(Name = "id")] ulong driverId)
        {
            UpdateDriversLicenseCommand command = _mapper.Map<UpdateDriversLicenseCommand>(request);
            command.DriverId = driverId;
            command.Id = licenseId;
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }
    }
}

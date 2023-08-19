using Application.DriverLicenses.Commands.Create;
using Application.DriverLicenses.Commands.Delete;
using Application.DriverLicenses.Commands.Update;
using Application.Drivers.Commands.AssignVehicle;
using Application.Drivers.Commands.Fire;
using Application.Drivers.Commands.UnassignVehicle;
using Application.Drivers.Queries.GetPerformanceOfDriver;
using AutoMapper;
using Domain.Constants;
using Domain.PlainObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Drivers;
using Presentation.Contracts.Licences.DriversLicences;
using System.Security.Claims;

namespace Presentation.Controllers
{
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
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

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
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
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
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpDelete("fire/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Fire([FromRoute] ulong id)
        {
            FireDriverCommand command = new()
            {
                Id = id,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPost("{id}/assign-vehicle/{vehicleId}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> AssignVehicleToDriver([FromRoute(Name = "id")] ulong driverId, [FromRoute(Name = "vehicleId")] ulong vehicleId)
        {
            AssignVehicleCommand command = new()
            {
                DriverId = driverId,
                VehicleId = vehicleId,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPost("{id}/unassign-vehicle")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UnassignVehicleToDriver([FromRoute(Name = "id")] ulong driverId)
        {
            UnassignVehicleCommand command = new()
            {
                DriverId = driverId,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpGet("{id}/performance")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetPerformance([FromRoute(Name = "id")] ulong driverId, [FromQuery] GetPerformanceDataOfDriverRequest request)
        {
            GetPerformanceDataOfDriverQuery query = _mapper.Map<GetPerformanceDataOfDriverQuery>(request);
            query.DriverId = driverId;
            query.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result<DriverPerformanceData> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }
    }
}

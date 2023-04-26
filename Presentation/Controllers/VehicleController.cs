using Application.Trailers.Commands.AddToVehicle;
using Application.Trailers.Commands.Create;
using Application.Vehicles.Commands.DeleteVehicle;
using Application.Vehicles.Commands.UpdateInformation;
using AutoMapper;
using Domain.Constants;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Trailers;
using Presentation.Contracts.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class VehicleController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VehicleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleRequest request, [FromRoute(Name = "id")] ulong vehicleId)
        {
            UpdateVehicleInformationCommand command = new()
            {
                Id = vehicleId,
                Model = request.Model,
                Manufacturer = request.Manufacturer,
                DateOfManufacturing = request.DateOfManufacturing,
                Dimensions = new Dimensions(request.Width, request.Depth),
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] ulong vehicleId)
        {
            DeleteVehicleCommand command = new()
            {
                VehicleId = vehicleId,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPost("{id}/trailer/{trailerId}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> CreateTrailer([FromRoute(Name = "id")] ulong vehicleId, [FromRoute(Name = "trailerId")] ulong trailerId)
        {
            AddTrailerToVehicleCommand command = new()
            {
                TrailerId = trailerId,
                VehicleId = vehicleId,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!),
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(CreateTrailer), null);
        }

    }
}

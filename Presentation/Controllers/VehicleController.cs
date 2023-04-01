using Application.Vehicles.Commands.DeleteVehicle;
using Application.Vehicles.Commands.UpdateInformation;
using AutoMapper;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
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

    }
}

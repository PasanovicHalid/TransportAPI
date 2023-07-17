using Application.Common.Interfaces.Persistence;
using Application.Trailers.Commands.AddToVehicle;
using Application.Trucks.Commands.AddToCompany;
using Application.Trucks.Commands.Delete;
using Application.Trucks.Commands.UpdateInformation;
using Application.Trucks.Queries.GetById;
using Application.Trucks.Queries.GetPage;
using Application.Vans.Commands.AddToCompany;
using Application.Vans.Commands.Delete;
using Application.Vans.Commands.UpdateInformation;
using Application.Vans.Queries.GetById;
using Application.Vans.Queries.GetPage;
using Application.Vehicles.Commands.DeleteVehicle;
using Application.Vehicles.Commands.UpdateInformation;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Common;
using Presentation.Contracts.Common.Models;
using Presentation.Contracts.Trucks;
using Presentation.Contracts.Vans;
using Presentation.Contracts.Vehicles;
using System.Security.Claims;

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

        [HttpPost("van/page")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetVans([FromBody] VanPageRequest request)
        {
            VanPageQuery query = _mapper.Map<VanPageQuery>(request);

            Result<PaginatedList<Van>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(new PaginatedResponse<VanResponse>(response.Value.Select(_mapper.Map<VanResponse>).ToList(),
                                                              response.Value.PageIndex,
                                                              request.PageSize,
                                                              response.Value.TotalCount));
        }

        [HttpPost("truck/page")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTrucks([FromBody] TruckPageRequest request)
        {
            TruckPageQuery query = _mapper.Map<TruckPageQuery>(request);

            Result<PaginatedList<Truck>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(new PaginatedResponse<VehicleResponse>(response.Value.Select(_mapper.Map<VehicleResponse>).ToList(),
                                                             response.Value.PageIndex,
                                                             request.PageSize,
                                                             response.Value.TotalCount));
        }

        [HttpGet("truck/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTruck(ulong id)
        {
            GetTruckByIdQuery request = new()
            {
                Id = id,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<Truck> response = await _mediator.Send(request);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<VehicleResponse>(response.Value));
        }

        [HttpGet("van/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetVan(ulong id)
        {
            GetVanByIdQuery request = new()
            {
                Id = id,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<Van> response = await _mediator.Send(request);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<VanResponse>(response.Value));
        }

        [HttpPost("truck")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> CreateTruck([FromBody] CreateTruckRequest request)
        {
            AddTruckToCompanyCommand command = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!),
                DateOfManufacturing = request.DateOfManufacturing,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                Dimensions = new Dimensions(request.Width, request.Depth)
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(CreateTruck), null);
        }

        [HttpPost("van")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> CreateVan([FromBody] CreateVanRequest request)
        {
            AddVanToCompanyCommand command = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!),
                DateOfManufacturing = request.DateOfManufacturing,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                Dimensions = new Dimensions(request.Width, request.Depth),
                Capacity = new Capacity(request.WidthCompartment, request.DepthCompartment, request.HeightCompartment, request.MaxCarryWeight)
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(CreateVan), null);
        }

        [HttpPut("van/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UpdateVan([FromBody] UpdateVanInformationRequest request, [FromRoute(Name = "id")] ulong vanId)
        {
            UpdateVanInformationCommand command = _mapper.Map<UpdateVanInformationCommand>(request);
            command.Id = vanId;
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok();
        }

        [HttpDelete("van/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> DeleteVan([FromRoute(Name = "id")] ulong vanId)
        {
            DeleteVanCommand command = new()
            {
                Id = vanId,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok();
        }

        [HttpDelete("truck/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> DeleteTruck([FromRoute(Name = "id")] ulong truckId)
        {
            DeleteTruckCommand command = new()
            {
                Id = truckId,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok();
        }

        [HttpPut("truck/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UpdateTruck([FromBody] UpdateTruckInformationRequest request, [FromRoute(Name = "id")] ulong truckId)
        {
            UpdateTruckInformationCommand command = _mapper.Map<UpdateTruckInformationCommand>(request);
            command.Id = truckId;
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok();
        }
    }
}

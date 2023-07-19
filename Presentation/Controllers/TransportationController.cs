using Application.Common.Interfaces.Persistence;
using Application.Companies.Commands.UpdateInformation;
using Application.Trailers.Queries.GetById;
using Application.Transportations.Commands.AddResolution;
using Application.Transportations.Commands.Create;
using Application.Transportations.Commands.Delete;
using Application.Transportations.Commands.Update;
using Application.Transportations.Queries.GetById;
using Application.Transportations.Queries.GetDashboardInfo;
using Application.Transportations.Queries.GetPage;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Common;
using Presentation.Contracts.Common.Models;
using Presentation.Contracts.Transportation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class TransportationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public TransportationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateTransportationRequest request)
        {
            CreateTransportationCommand command = _mapper.Map<CreateTransportationCommand>(request);
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return CreatedAtAction(nameof(Create), null);
        }

        [HttpPost("{id}/resolution")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> ResolveTransport([FromBody] AddResolutionRequest request, [FromRoute(Name = "id")] ulong transportationId)
        {
            AddResolutionToTransportationCommand command = _mapper.Map<AddResolutionToTransportationCommand>(request);
            command.TransportationId = transportationId;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UpdateInformation([FromBody] UpdateTransportationRequest request, [FromRoute(Name = "id")] ulong transportationId)
        {
            UpdateTransportationCommand command = _mapper.Map<UpdateTransportationCommand>(request);
            command.TransportationId = transportationId;
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> DeleteTransportation([FromRoute(Name = "id")] ulong transportationId)
        {
            DeleteTransportationCommand command = new(); 
            command.TransportationId = transportationId;
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTransportById([FromRoute(Name = "id")] ulong transportationId)
        {
            GetTransportationByIdQuery query = new GetTransportationByIdQuery
            {
                Id = transportationId 
            };

            Result<Transportation> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<TransportationResponse>(response.Value));
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTransportDashboardInfo()
        {
            GetTransportationDashboardInfoQuery query = new GetTransportationDashboardInfoQuery
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<TransportationDashboardInfo> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }

        [HttpPost("page")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetPage([FromBody] TransportationPageRequest request)
        {
            GetTransportPageQuery query = _mapper.Map<GetTransportPageQuery>(request);
            query.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result<PaginatedList<Transportation>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(new PaginatedResponse<TransportationResponse>(response.Value.Select(_mapper.Map<TransportationResponse>).ToList(),
                                                              response.Value.PageIndex,
                                                              request.PageSize,
                                                              response.Value.TotalCount));
        }
    }
}

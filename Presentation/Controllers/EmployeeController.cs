using Application.Drivers.Commands.Fire;
using Application.Employees.Commands.UpdateInformationById;
using Application.Employees.Commands.UpdateInformationByIdentity;
using Application.Employees.Queries.FindById;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public EmployeeController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("information/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UpdateInformationByAdmin([FromBody] UpdateEmployeeInformationRequest request, [FromRoute(Name = "id")] ulong employeeId)
        {
            UpdateEmployeeInformationByIdCommand command = _mapper.Map<UpdateEmployeeInformationByIdCommand>(request);
            command.Id = employeeId;
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPut("information")]
        [Authorize(Roles = ApplicationRolesConstants.Driver)]
        public async Task<IActionResult> UpdateInformationByDrvier([FromBody] UpdateEmployeeInformationRequest request)
        {
            UpdateEmployeeInformationByIdentityCommand command = _mapper.Map<UpdateEmployeeInformationByIdentityCommand>(request);
            command.IdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpGet("information/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetInformation([FromRoute(Name = "id")] ulong employeeId)
        {
            FindEmployeeByIdQuery query = new()
            {
                Id = employeeId,
                AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
            };

            Result<Employee> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }
    }
}

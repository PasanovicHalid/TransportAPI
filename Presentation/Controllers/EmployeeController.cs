using Application.Common.Interfaces.Persistence;
using Application.Employees.Commands.UpdateInformationById;
using Application.Employees.Commands.UpdateInformationByIdentity;
using Application.Employees.Queries.FindById;
using Application.Employees.Queries.GetDashboardInfo;
using Application.Employees.Queries.GetPage;
using Application.Vehicles.Queries.GetDashboardInfo;
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
using Presentation.Contracts.Employees;
using System.Security.Claims;

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
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpPut("information")]
        [Authorize(Roles = ApplicationRolesConstants.Driver)]
        public async Task<IActionResult> UpdateInformationByDriver([FromBody] UpdateEmployeeInformationRequest request)
        {
            UpdateEmployeeInformationByIdentityCommand command = _mapper.Map<UpdateEmployeeInformationByIdentityCommand>(request);
            command.IdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!;

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
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<Employee> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<EmployeeResponse>(response.Value));
        }

        [HttpGet("information")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetInformationByEmployee()
        {
            throw new NotImplementedException();
            FindEmployeeByIdQuery query = new()
            {
                Id = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!),
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<Employee> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<EmployeeResponse>(response.Value));
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetEmployeeDashboardInfo()
        {
            GetEmployeeDashboardInfoQuery query = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<EmployeeDashboardInfo> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }

        [HttpPost("page")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetPage([FromBody] EmployeePageRequest request)
        {
            EmployeePageQuery query = _mapper.Map<EmployeePageQuery>(request);
            query.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result<PaginatedList<Employee>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);


            return Ok(new PaginatedResponse<EmployeeResponse>(response.Value.Select(_mapper.Map<EmployeeResponse>).ToList(),
                                                              response.Value.PageIndex,
                                                              request.PageSize,
                                                              response.Value.TotalCount));

        }
    }
}

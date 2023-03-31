using Application.Authentication.Commands.Register.Admins;
using Application.Authentication.Commands.Register.Drivers;
using Application.Authentication.Contracts;
using Application.Common.Commands;
using Application.Common.Interfaces.Persistence;
using Application.Companies.Commands.Create;
using Application.Companies.Commands.Remove;
using Application.Companies.Commands.UpdateInformation;
using Application.Companies.Queries.FindById;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Authentication;
using Presentation.Contracts.Companies;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class CompanyController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CompanyController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
        {
            CreateCompanyCommand command = _mapper.Map<CreateCompanyCommand>(request);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return CreatedAtAction(nameof(Create), null);
        }

        [HttpPut]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyRequest request)
        {
            UpdateCompanyInformationCommand command = _mapper.Map<UpdateCompanyInformationCommand>(request);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> Delete([FromRoute] ulong id)
        {
            Result response = await _mediator.Send(new RemoveCompanyCommand(id));

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] ulong id)
        {
            Result<Company> response = await _mediator.Send(new FindCompanyByIdCommand(id));

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetPage()
        {
            Result<PaginatedList<Company>> response = await _mediator.Send(new PageCommand<Company>());

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }

        [HttpPost("{id}/register/initial/admin")]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> RegisterInitialAdminForCompany([FromBody] AdminRegistrationRequest request, [FromRoute(Name = "id")] ulong companyId)
        {
            RegisterAdminCommand command = _mapper.Map<RegisterAdminCommand>(request);
            command.CompanyId = companyId;

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("{id}/register/admin")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegistrationRequest request, [FromRoute(Name = "id")] ulong companyId)
        {
            RegisterAdminCommand command = _mapper.Map<RegisterAdminCommand>(request);
            command.CompanyId = companyId;

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register/driver")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> RegisterDriver([FromBody] DriverRegistrationRequest request)
        {
            RegisterDriverCommand command = _mapper.Map<RegisterDriverCommand>(request);
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterAdmin), _mapper.Map<AutheticationResponse>(result.Value));
        }
    }
}

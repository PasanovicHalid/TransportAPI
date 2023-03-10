using Application.Common.Commands;
using Application.Common.Interfaces.Persistance;
using Application.Companies.Commands.Create;
using Application.Companies.Commands.Delete;
using Application.Companies.Commands.Update;
using Application.Companies.Queries.FindById;
using AutoMapper;
using Domain.Companies;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Companies;

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

        [HttpPut("update/superAdmin")]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyRequest request)
        {
            UpdateCompanyCommand command = _mapper.Map<UpdateCompanyCommand>(request);

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRolesConstants.SuperAdmin)]
        public async Task<IActionResult> Delete([FromRoute] ulong id)
        {
            Result response = await _mediator.Send(new DeleteCompanyCommand(id));

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
    }
}

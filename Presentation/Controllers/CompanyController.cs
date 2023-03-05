using Application.Authentication.Contracts;
using Application.Common.Commands;
using Application.Common.Interfaces.Persistance;
using Application.Companies.Commands.Create;
using Application.Companies.Commands.Delete;
using Application.Companies.Commands.Update;
using Application.Companies.Queries.FindCompanyById;
using AutoMapper;
using Domain;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Authentication;
using Presentation.Contracts.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

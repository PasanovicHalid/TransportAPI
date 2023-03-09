using Application.Companies.Commands.Create;
using Application.Licences.DriverLicences.Commands.Create;
using AutoMapper;
using Domain.Constants;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Companies;
using Presentation.Contracts.Licences.DriversLicences;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.Licences
{
    public class DriversLicenceController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DriversLicenceController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateDriversLicenceRequest request)
        {
            CreateDriversLicenceCommand command = _mapper.Map<CreateDriversLicenceCommand>(request);
            command.AdminIdentityId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            Result response = await _mediator.Send(command);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return CreatedAtAction(nameof(Create), null);
        }
    }
}

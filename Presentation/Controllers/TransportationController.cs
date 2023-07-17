using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateTransportationRequest request)
        {
            CreateTransportationCommand command = _mapper.Map<CreateTransportationCommand>(request);
            Result response = await _mediator.Send(command);
            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);
            return CreatedAtAction(nameof(Create), null);
        }
    }
}

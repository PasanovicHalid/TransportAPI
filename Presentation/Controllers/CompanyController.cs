using Application.Authentication.Commands.Register.Admins;
using Application.Authentication.Commands.Register.Drivers;
using Application.Authentication.Contracts;
using Application.Common.Interfaces.Persistence;
using Application.Companies.Commands.Create;
using Application.Companies.Commands.Remove;
using Application.Companies.Commands.UpdateInformation;
using Application.Companies.Queries.FindById;
using Application.Companies.Queries.GetPage;
using Application.Dashboard.Queries.GetDashboardInfo;
using Application.Drivers.Queries.GetDriversByCompany;
using Application.Drivers.Queries.GetDriversWithoutVehicles;
using Application.Trailers.Commands.Create;
using Application.Trailers.Commands.Delete;
using Application.Trailers.Commands.Update;
using Application.Trailers.Queries.GetById;
using Application.Trailers.Queries.GetPage;
using Application.Vehicles.Queries.GetAllByCompany;
using Application.Vehicles.Queries.GetFreeVehicles;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Controllers;
using Presentation.Contracts.Authentication;
using Presentation.Contracts.Common;
using Presentation.Contracts.Common.Models;
using Presentation.Contracts.Companies;
using Presentation.Contracts.Dashboard;
using Presentation.Contracts.Trailers;
using System.ComponentModel.Design;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Presentation.Controllers
{
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
            Result<Company> response = await _mediator.Send(new FindCompanyByIdQuery(id));

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<CompanyResponse>(response.Value));
        }

        [HttpGet("drivers-by-company")]
        public async Task<IActionResult> GetDriversByCompany()
        {
            ulong companyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);
            Result<List<Driver>> response = await _mediator.Send(new GetDriversByCompanyQuery(companyId));

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<List<EmployeeResponse>>(response.Value));
        }

        [HttpGet("drivers-without-vehicles")]
        public async Task<IActionResult> GetDriversWithoutVehicles()
        {
            GetDriversWithoutVehiclesQuery query = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<List<Driver>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<List<EmployeeResponse>>(response.Value));
        }

        [HttpGet("free-vehicles-by-company")]
        public async Task<IActionResult> GetFreeVehiclesByCompany()
        {
            GetFreeVehiclesByCompanyQuery query = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };
            
            Result<List<Vehicle>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<List<VehicleResponse>>(response.Value));
        }

        [HttpGet("vehicles-of-company")]
        public async Task<IActionResult> GetAllVehiclesByCompany()
        {
            GetAllVehiclesByCompanyQuery query = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<List<Vehicle>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<List<VehicleResponse>>(response.Value));
        }

        [HttpPost("page")]
        public async Task<IActionResult> GetPage()
        {
            Result<PaginatedList<Company>> response = await _mediator.Send(new CompanyPageQuery());

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<List<CompanyResponse>>(response.Value));
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

            return CreatedAtAction(nameof(RegisterInitialAdminForCompany), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("register/admin")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegistrationRequest request)
        {
            RegisterAdminCommand command = _mapper.Map<RegisterAdminCommand>(request);
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

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
            command.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result<AuthenticationResult> result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(RegisterDriver), _mapper.Map<AutheticationResponse>(result.Value));
        }

        [HttpPost("trailer")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> CreateTrailer([FromBody] CreateTrailerRequest request)
        {
            CreateTrailerForCompanyCommand command = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!),
                Capacity = new Capacity(request.Width, request.Depth, request.Height, request.MaxCarryWeight)
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return CreatedAtAction(nameof(CreateTrailer), null);
        }

        [HttpDelete("trailer/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> DeleteTrailer([FromRoute(Name = "id")] ulong trailerId)
        {
            DeleteTrailerCommand command = new()
            {
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!),
                TrailerId = trailerId
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok();
        }

        [HttpPut("trailer/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> UpdateTrailer([FromBody] UpdateTrailerRequest request, [FromRoute(Name = "id")] ulong trailerId)
        {
            UpdateTrailerCommand command = new()
            {
                Capacity = new Capacity(request.Width, request.Depth, request.Height, request.MaxCarryWeight),
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!),
                TrailerId = trailerId
            };

            Result result = await _mediator.Send(command);

            if (result.IsFailed)
                return HandleErrors(result.Errors[0]);

            return Ok();
        }

        [HttpGet("trailer/{id}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTrailer(ulong id)
        {
            GetTrailerByIdQuery request = new()
            {
                Id = id,
                CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!)
            };

            Result<Trailer> response = await _mediator.Send(request);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(_mapper.Map<TrailerResponse>(response.Value));
        }

        [HttpPost("trailer/page")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTrailers([FromBody] TrailerPageRequest request)
        {
            TrailerPageQuery query = _mapper.Map<TrailerPageQuery>(request);

            Result<PaginatedList<Trailer>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(new PaginatedResponse<TrailerResponse>(response.Value.Select(_mapper.Map<TrailerResponse>).ToList(),
                                                             response.Value.PageIndex,
                                                             request.PageSize,
                                                             response.Value.TotalCount));
        }

        [HttpPost("trailer/page-assigned-to-vehicle/{vehicleId}")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetTrailersForVehicle([FromBody] TrailerPageRequest request, [FromRoute(Name = "vehicleId")] ulong vehicleId)
        {
            TrailerPageQuery query = _mapper.Map<TrailerPageQuery>(request);
            query.Filter = x => x.VehicleId == vehicleId;

            Result<PaginatedList<Trailer>> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(new PaginatedResponse<TrailerResponse>(response.Value.Select(_mapper.Map<TrailerResponse>).ToList(),
                                                             response.Value.PageIndex,
                                                             request.PageSize,
                                                             response.Value.TotalCount));
        }

        [HttpPost("dashboard")]
        [Authorize(Roles = ApplicationRolesConstants.Admin)]
        public async Task<IActionResult> GetDashboardInfo([FromBody] DashboardRequest request)
        {
            GetDashboardInfoQuery query = _mapper.Map<GetDashboardInfoQuery>(request);
            query.CompanyId = ulong.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value!);

            Result<DashboardInfo> response = await _mediator.Send(query);

            if (response.IsFailed)
                return HandleErrors(response.Errors[0]);

            return Ok(response.Value);
        }
    }
}

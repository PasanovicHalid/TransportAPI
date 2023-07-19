using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Queries.GetDriversByCompany
{
    public class GetDriversByCompanyQueryHandler : IRequestHandler<GetDriversByCompanyQuery, Result<List<Driver>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDriversByCompanyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Driver>>> Handle(GetDriversByCompanyQuery request, CancellationToken cancellationToken)
        {
            List<Driver> drivers = await _unitOfWork.Drivers.GetAllAsync(d => d.CompanyId == request.CompanyId);

            return Result.Ok(drivers);
        }
    }
}

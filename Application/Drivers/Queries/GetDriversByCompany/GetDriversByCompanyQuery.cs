using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.Queries.GetDriversByCompany
{
    public class GetDriversByCompanyQuery : IRequest<Result<List<Driver>>>
    {
        public GetDriversByCompanyQuery(ulong companyId)
        {
            CompanyId = companyId;
        }

        public ulong CompanyId { get; set; }
    }

    public class GetDriversByCompanyValidator : AbstractValidator<GetDriversByCompanyQuery>
    {
        public GetDriversByCompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

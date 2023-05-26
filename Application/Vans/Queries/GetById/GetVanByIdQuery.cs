using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vans.Queries.GetById
{
    public class GetVanByIdQuery : IRequest<Result<Van>>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class GetVanByIdValidator : AbstractValidator<GetVanByIdQuery>
    {
        public GetVanByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

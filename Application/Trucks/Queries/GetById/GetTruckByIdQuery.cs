using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trucks.Queries.GetById
{
    public class GetTruckByIdQuery : IRequest<Result<Truck>>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class GetTruckByIdValidator : AbstractValidator<GetTruckByIdQuery>
    {
        public GetTruckByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

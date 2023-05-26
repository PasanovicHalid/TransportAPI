using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trailers.Queries.GetById
{
    public class GetTrailerByIdQuery : IRequest<Result<Trailer>>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }
    
    public class GetTrailerByIdValidator : AbstractValidator<GetTrailerByIdQuery>
    {
        public GetTrailerByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

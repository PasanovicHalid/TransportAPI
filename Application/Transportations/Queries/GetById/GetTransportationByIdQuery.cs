using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Transportations.Queries.GetById
{
    public class GetTransportationByIdQuery : IRequest<Result<Transportation>>
    {
        public ulong Id { get; set; }
    }

    public class GetTransportationByIdQueryValidator : AbstractValidator<GetTransportationByIdQuery>
    {
        public GetTransportationByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

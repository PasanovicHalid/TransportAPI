using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

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

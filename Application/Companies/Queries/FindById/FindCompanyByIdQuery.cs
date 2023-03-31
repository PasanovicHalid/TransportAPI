using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Companies.Queries.FindById
{
    public class FindCompanyByIdQuery : IRequest<Result<Company>>
    {
        public ulong Id { get; set; }

        public FindCompanyByIdQuery()
        {
        }

        public FindCompanyByIdQuery(ulong id)
        {
            Id = id;
        }
    }

    public class FindCompanyByIdValidator : AbstractValidator<FindCompanyByIdQuery>
    {
        public FindCompanyByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

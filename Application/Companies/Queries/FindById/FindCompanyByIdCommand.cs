using Domain.Companies;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Companies.Queries.FindById
{
    public class FindCompanyByIdCommand : IRequest<Result<Company>>
    {
        public ulong Id { get; set; }

        public FindCompanyByIdCommand()
        {
        }

        public FindCompanyByIdCommand(ulong id)
        {
            Id = id;
        }
    }

    public class FindCompanyByIdValidator : AbstractValidator<FindCompanyByIdCommand>
    {
        public FindCompanyByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

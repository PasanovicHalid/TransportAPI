using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Companies.Commands.Delete
{
    public class DeleteCompanyCommand : IRequest<Result>
    {
        public ulong Id { get; set; }

        public DeleteCompanyCommand()
        {
        }

        public DeleteCompanyCommand(ulong id)
        {
            Id = id;
        }
    }

    public class DeleteCompanyValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

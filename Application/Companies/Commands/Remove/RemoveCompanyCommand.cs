using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Companies.Commands.Remove
{
    public class RemoveCompanyCommand : IRequest<Result>
    {
        public ulong Id { get; set; }

        public RemoveCompanyCommand()
        {
        }

        public RemoveCompanyCommand(ulong id)
        {
            Id = id;
        }
    }

    public class RemoveCompanyValidator : AbstractValidator<RemoveCompanyCommand>
    {
        public RemoveCompanyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

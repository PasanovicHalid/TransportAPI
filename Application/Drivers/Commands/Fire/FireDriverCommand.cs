using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Drivers.Commands.Fire
{
    public class FireDriverCommand : IRequest<Result>
    {
        public ulong Id { get; set; }

        public ulong CompanyId { get; set; }
    }

    public class FireDriverValidator : AbstractValidator<FireDriverCommand>
    {
        public FireDriverValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}


using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Vans.Commands.Delete
{
    public class DeleteVanCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class DeleteVanValidator : AbstractValidator<DeleteVanCommand>
    {
        public DeleteVanValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

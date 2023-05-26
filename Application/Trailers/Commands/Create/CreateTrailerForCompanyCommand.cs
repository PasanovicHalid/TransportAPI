using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trailers.Commands.Create
{
    public class CreateTrailerForCompanyCommand : IRequest<Result>
    {
        public ulong CompanyId { get; set; }
        public Capacity Capacity { get; set; }
    }

    public class CreateTrailerForCompanyValidator : AbstractValidator<CreateTrailerForCompanyCommand>
    {
        public CreateTrailerForCompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Capacity).NotNull();
            RuleFor(x => x.Capacity.MaxCarryWeight).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume).NotNull();
            RuleFor(x => x.Capacity.Volume.Depth).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume.Width).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume.Height).NotEmpty().GreaterThan(0);
        }
    }

}



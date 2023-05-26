using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trailers.Commands.Update
{
    public class UpdateTrailerCommand : IRequest<Result>
    {
        public ulong TrailerId { get; set; }
        public Capacity Capacity { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class UpdateTrailerValidator : AbstractValidator<UpdateTrailerCommand>
    {
        public UpdateTrailerValidator()
        {
            RuleFor(x => x.TrailerId).NotEmpty();
            RuleFor(x => x.Capacity).NotNull();
            RuleFor(x => x.Capacity.MaxCarryWeight).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume).NotNull();
            RuleFor(x => x.Capacity.Volume.Depth).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume.Width).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Capacity.Volume.Height).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }

}



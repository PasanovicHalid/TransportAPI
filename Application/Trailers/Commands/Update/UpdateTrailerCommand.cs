using MediatR;
using FluentResults;
using FluentValidation;
using Domain.ValueObjects;

namespace Application.Trailers.Commands.Update
{
    public class UpdateTrailerCommand : IRequest<Result>
    {
        public ulong TrailerId { get; set; }
        public Capacity Capacity { get; set; }
    }

    public class UpdateTrailerValidator : AbstractValidator<UpdateTrailerCommand>
    {
        public UpdateTrailerValidator()
        {
            RuleFor(x => x.TrailerId).NotEmpty();
            RuleFor(x => x.Capacity).NotEmpty();
        }
    }

}



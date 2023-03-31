using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trailers.Commands.Delete
{
    public class DeleteTrailerCommand : IRequest<Result>
    {
        public ulong TrailerId { get; set; }
    }

    public class DeleteTrailerValidator : AbstractValidator<DeleteTrailerCommand>
    {
        public DeleteTrailerValidator()
        {
            RuleFor(x => x.TrailerId).NotEmpty();
        }
    }

}



using Application.Common.Queries;
using Domain.Entities;
using FluentValidation;

namespace Application.Trailers.Queries.GetPage
{
    public class TrailerPageQuery : PageQuery<Trailer>
    {
        public TrailerPageQuery() : base()
        {
        }
    }

    public class TrailerPageRequestValidator : AbstractValidator<TrailerPageQuery>
    {
        private HashSet<string> _entityFields = new();
        public TrailerPageRequestValidator()
        {
            _entityFields = new HashSet<string>(typeof(Trailer).GetProperties().Select(x => x.Name));
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.IncludeProperties).Must(x => _entityFields.IsSupersetOf(x!))
                                             .When(x => x.IncludeProperties is not null)
                                             .WithMessage("Included fields need to exist and shouldn't be duplicates.");
        }
    }
}

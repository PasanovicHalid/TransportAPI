using Application.Common.Queries;
using Domain.Entities;
using FluentValidation;

namespace Application.Vans.Queries.GetPage
{
    public class VanPageQuery : PageQuery<Van>
    {
        public VanPageQuery() : base()
        {
        }
    }

    public class VanPageRequestValidator : AbstractValidator<VanPageQuery>
    {
        private HashSet<string> _entityFields = new();
        public VanPageRequestValidator()
        {
            _entityFields = new HashSet<string>(typeof(Van).GetProperties().Select(x => x.Name));
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.IncludeProperties).Must(x => _entityFields.IsSupersetOf(x!))
                                             .When(x => x.IncludeProperties is not null)
                                             .WithMessage("Included fields need to exist and shouldn't be duplicates.");
        }
    }

}

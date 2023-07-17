using Application.Common.Queries;
using Domain.Entities;
using FluentValidation;

namespace Application.Companies.Queries.GetPage
{
    public class CompanyPageQuery : PageQuery<Company>
    {
        public CompanyPageQuery() : base()
        {
        }
    }

    public class CompanyPageRequestValidator : AbstractValidator<CompanyPageQuery>
    {
        private HashSet<string> _entityFields = new();
        public CompanyPageRequestValidator()
        {
            _entityFields = new HashSet<string>(typeof(Company).GetProperties().Select(x => x.Name));

            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.IncludeProperties).Must(x => _entityFields.IsSupersetOf(x!))
                                             .When(x => x.IncludeProperties is not null)
                                             .WithMessage("Included fields need to exist and shouldn't be duplicates.");
        }
    }
}

using Application.Common.Queries;
using Domain.Entities;
using FluentValidation;

namespace Application.Employees.Queries.GetPage
{
    public class EmployeePageQuery : PageQuery<Employee>
    {
        public ulong CompanyId { get; set; }
        public EmployeePageQuery() : base()
        {
        }
    }

    public class EmployeePageRequestValidator : AbstractValidator<EmployeePageQuery>
    {
        private HashSet<string> _entityFields = new();
        public EmployeePageRequestValidator()
        {
            _entityFields = new HashSet<string>(typeof(Employee).GetProperties().Select(x => x.Name));
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.IncludeProperties).Must(x => _entityFields.IsSupersetOf(x!))
                                             .When(x => x.IncludeProperties is not null)
                                             .WithMessage("Included fields need to exist and shouldn't be duplicates.");
        }
    }
}

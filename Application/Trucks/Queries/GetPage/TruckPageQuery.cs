using Application.Common.Queries;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trucks.Queries.GetPage
{
    public class TruckPageQuery : PageQuery<Truck>
    {
        public TruckPageQuery() : base()
        {
        }
    }
    
    public class TruckPageRequestValidator : AbstractValidator<TruckPageQuery>
    {
        private HashSet<string> _entityFields = new();
        public TruckPageRequestValidator()
        {
            _entityFields = new HashSet<string>(typeof(Truck).GetProperties().Select(x => x.Name));
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.IncludeProperties).Must(x => _entityFields.IsSupersetOf(x!))
                                             .When(x => x.IncludeProperties is not null)
                                             .WithMessage("Included fields need to exist and shouldn't be duplicates.");
        }
    }
}

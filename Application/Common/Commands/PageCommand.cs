using Application.Common.Interfaces.Persistance;
using Domain;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Commands
{
    public class PageCommand<T> : IRequest<Result<PaginatedList<T>>> where T : class
    {
        public Expression<Func<T, bool>>? Filter { get; set; }

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public bool Desc { get; set; }

        public List<string>? IncludeProperties { get; set; }

        public bool WithDeleted { get; set; }

        public bool Tracked { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }

    public class PageValidator<T> : AbstractValidator<PageCommand<T>> where T : class
    {
        public PageValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}

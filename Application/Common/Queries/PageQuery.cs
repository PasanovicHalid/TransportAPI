using Application.Common.Interfaces.Persistence;
using FluentResults;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries
{
    public class PageQuery<T> : IRequest<Result<PaginatedList<T>>> where T : class
    {
        public Expression<Func<T, bool>>? Filter { get; set; }

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public bool Desc { get; set; }

        public List<string>? IncludeProperties { get; set; }

        public bool WithDeleted { get; set; }

        public bool Tracked { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}

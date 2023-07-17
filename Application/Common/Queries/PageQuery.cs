using Application.Common.Interfaces.Persistence;
using FluentResults;
using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries
{
    public class PageQuery<T> : IRequest<Result<PaginatedList<T>>> where T : class
    {
        public Expression<Func<T, bool>>? Filter { get; set; }

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public bool Desc { get; set; } = false;

        public List<string>? IncludeProperties { get; set; }

        public bool WithDeleted { get; set; } = false;

        public bool Tracked { get; set; } = false;

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}

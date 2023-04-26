using Domain.Common;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistence
{
    public interface IEntityRepository<T> where T : EntityObject
    {
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter,
                             List<string>? includeProperties = null,
                             bool canBeDeleted = false,
                             bool tracked = true,
                             CancellationToken cancellationToken = default);

        Task<PaginatedList<T>> GetPageAsync(Expression<Func<T, bool>>? filter = null,
                                       Expression<Func<T, object>>? orderBy = null,
                                       bool desc = false,
                                       List<string>? includeProperties = null,
                                       bool withDeleted = false,
                                       bool tracked = true,
                                       int pageIndex = 1,
                                       int pageSize = 10,
                                       CancellationToken cancellationToken = default);

        IQueryable<T> FinalizeQuery(IQueryable<T> query,
                                          bool includeDeleted,
                                          bool isTracked);

        void RemovePermanent(T item);

        void RemoveRangePermanent(IEnumerable<T> items);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

        Task AddAsync(T item,
                      CancellationToken cancellationToken = default);

        void Update(T item);
    }
}

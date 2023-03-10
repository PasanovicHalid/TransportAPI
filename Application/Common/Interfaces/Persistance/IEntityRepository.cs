using Domain.Common;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistance
{
    public interface IEntityRepository<T> where T : EntityObject
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                              Expression<Func<T, object>>? orderBy = null,
                              bool desc = false,
                              List<string>? includeProperties = null,
                              bool withDeleted = false,
                              bool tracked = true);

        T? GetFirstOrDefault(Expression<Func<T, bool>> filter,
                             List<string>? includeProperties = null,
                             bool canBeDeleted = false,
                             bool tracked = true);

        Task<PaginatedList<T>> GetPage(Expression<Func<T, bool>>? filter = null,
                                       Expression<Func<T, object>>? orderBy = null,
                                       bool desc = false,
                                       List<string>? includeProperties = null,
                                       bool withDeleted = false,
                                       bool tracked = true,
                                       int pageIndex = 1,
                                       int pageSize = 10);

        void RemovePermanent(T item);

        void RemoveRangePermanent(IEnumerable<T> items);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

        void Add(T item);

        void Update(T item);
    }
}

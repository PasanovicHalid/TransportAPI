using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistence
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, List<string>? includeProperties = null, bool tracked = true);

        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, List<string>? includeProperties = null, bool tracked = true);

        void RemovePermanent(T item);

        void RemoveRangePermanent(IEnumerable<T> items);

        void Add(T item);

        void Update(T item);
    }
}

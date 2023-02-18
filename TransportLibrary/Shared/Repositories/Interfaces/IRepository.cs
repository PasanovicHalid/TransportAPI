using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Shared.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, List<IncludedProperty>? includeProperties = null, bool tracked = true);

        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, List<IncludedProperty>? includeProperties = null, bool tracked = true);

        void RemovePermanent(T item);

        void RemoveRangePermanent(IEnumerable<T> items);

        void Add(T item);

        void Update(T item);
    }
}

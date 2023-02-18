using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared.ModelBase;

namespace TransportLibrary.Shared.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : EntityObject
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, List<IncludedProperty>? includeProperties = null, bool withDeleted = false, bool tracked = true);

        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, List<IncludedProperty>? includeProperties = null, bool canBeDeleted = false, bool tracked = true);

        void RemovePermanent(T item);

        void RemoveRangePermanent(IEnumerable<T> items);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

        void Add(T item);

        void Update(T item);
    }
}

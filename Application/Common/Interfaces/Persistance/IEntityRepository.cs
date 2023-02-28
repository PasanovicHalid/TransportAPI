using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Common.Interfaces.Persistance
{
    public interface IEntityRepository<T> where T : EntityObject
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, List<string>? includeProperties = null, bool withDeleted = false, bool tracked = true);

        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, List<string>? includeProperties = null, bool canBeDeleted = false, bool tracked = true);

        void RemovePermanent(T item);

        void RemoveRangePermanent(IEnumerable<T> items);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

        void Add(T item);

        void Update(T item);
    }
}

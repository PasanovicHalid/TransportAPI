using System.Linq.Expressions;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TransportDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(TransportDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, List<string>? includeProperties = null, bool tracked = true)
        {
            IQueryable<T> query = SetupTracking(tracked);

            if (filter != null)
                query = query.Where(filter);

            query = IncludeProperties(includeProperties, query);

            return query.ToList();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, List<string>? includeProperties = null, bool tracked = true)
        {
            IQueryable<T> query = SetupTracking(tracked);

            query = query.Where(filter);

            query = IncludeProperties(includeProperties, query);

            return query.FirstOrDefault();
        }
        public void RemovePermanent(T item)
        {
            _dbSet.Remove(item);
        }

        public void RemoveRangePermanent(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }

        private static IQueryable<T> IncludeProperties(List<string>? includeProperties, IQueryable<T> query)
        {
            if (includeProperties is { Count: > 0 })
            {
                foreach (string property in includeProperties)
                {
                    query = query.Include(property);
                }
            }

            return query;
        }

        private IQueryable<T> SetupTracking(bool tracked)
        {
            return tracked ? _dbSet : _dbSet.AsNoTracking();
        }
    }
}

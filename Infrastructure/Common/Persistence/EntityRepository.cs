using Application.Common.Interfaces.Persistence;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Common.Persistence
{
    public class EntityRepository<T> : IEntityRepository<T> where T : EntityObject
    {
        private readonly TransportDbContext _db;
        private DbSet<T> _dbSet;

        public EntityRepository(TransportDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public IQueryable<T> FinalizeQuery(IQueryable<T> query,
                                                 bool includeDeleted,
                                                 bool isTracked)
        {
            query = isTracked ? query.AsNoTracking() : query;
            return FilterDeleted(includeDeleted, query);
        }

        public async Task AddAsync(T item,
                                   CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(item, cancellationToken);
        }

        public void RemovePermanent(T item)
        {
            _dbSet.Remove(item);
        }

        public void RemoveRangePermanent(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public async Task<PaginatedList<T>> GetPageAsync(Expression<Func<T, bool>>? filter = null,
                                      Expression<Func<T, object>>? orderBy = null,
                                      bool desc = false,
                                      List<string>? includeProperties = null,
                                      bool withDeleted = false,
                                      bool tracked = true,
                                      int pageIndex = 1,
                                      int pageSize = 10,
                                      CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = SetupTracking(tracked);

            query = SetupSorting(orderBy, desc, query);

            if (filter != null)
                query = query.Where(filter);

            query = FilterDeleted(withDeleted, query);

            query = IncludeProperties(includeProperties, query);

            return await PaginatedList<T>.CreateAsync(query, pageIndex, pageSize, cancellationToken);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter,
                                    List<string>? includeProperties = null,
                                    bool canBeDeleted = false,
                                    bool tracked = true,
                                    CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = SetupTracking(tracked);

            query = query.Where(filter);

            query = FilterDeleted(canBeDeleted, query);

            query = IncludeProperties(includeProperties, query);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public void Remove(T item)
        {
            item.Deleted = true;
            _dbSet.Update(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                item.Deleted = true;
            }
            _dbSet.UpdateRange(items);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }

        private IQueryable<T> IncludeProperties(List<string>? includeProperties, IQueryable<T> query)
        {
            if (includeProperties != null && includeProperties.Count > 0)
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
            IQueryable<T> query;

            if (tracked)
            {
                query = _dbSet;
            }
            else
            {
                query = _dbSet.AsNoTracking();
            }

            return query;
        }

        private static IQueryable<T> FilterDeleted(bool deleted, IQueryable<T> query)
        {
            return deleted ? query : query.Where(u => u.Deleted == false);
        }

        private static IQueryable<T> SetupSorting(Expression<Func<T, object>>? orderBy, bool desc, IQueryable<T> query)
        {
            if (orderBy != null)
                if (desc)
                    return query.OrderByDescending(orderBy);
                else
                    return query.OrderBy(orderBy);
            return query;
        }

    }
}

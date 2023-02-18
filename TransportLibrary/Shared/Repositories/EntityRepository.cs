using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Settings;
using TransportLibrary.Shared.ModelBase;
using TransportLibrary.Shared.Repositories.Interfaces;

namespace TransportLibrary.Shared.Repositories
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

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void RemovePermanent(T item)
        {
            _dbSet.Remove(item);
        }

        public void RemoveRangePermanent(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                                     List<IncludedProperty>? includeProperties = null,
                                     bool withDeleted = false,
                                     bool tracked = true)
        {
            IQueryable<T> query = SetupTracking(tracked);

            if (filter != null)
                query = query.Where(filter);

            query = FilterDeleted(withDeleted, query);

            query = IncludeProperties(includeProperties, query);

            return query.ToList();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter,
                                    List<IncludedProperty>? includeProperties = null,
                                    bool canBeDeleted = false,
                                    bool tracked = true)
        {
            IQueryable<T> query = SetupTracking(tracked);

            query = query.Where(filter);

            query = FilterDeleted(canBeDeleted, query);

            query = IncludeProperties(includeProperties, query);

            return query.FirstOrDefault();
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

        private IQueryable<T> IncludeProperties(List<IncludedProperty>? includeProperties, IQueryable<T> query)
        {
            if (includeProperties != null && includeProperties.Count > 0)
            {
                foreach (IncludedProperty property in includeProperties)
                {
                    query = query.Include(property.Name);
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
            if (!deleted)
                query = query.Where(u => u.Deleted == false);
            return query;
        }
    }
}

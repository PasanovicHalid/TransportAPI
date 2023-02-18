using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Settings;
using TransportLibrary.Shared.Repositories.Interfaces;

namespace TransportLibrary.Shared.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TransportDbContext _db;
        private DbSet<T> _dbSet;

        public Repository(TransportDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, List<IncludedProperty>? includeProperties = null, bool tracked = true)
        {
            IQueryable<T> query = SetupTracking(tracked);

            if (filter != null)
                query = query.Where(filter);

            query = IncludeProperties(includeProperties, query);

            return query.ToList();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, List<IncludedProperty>? includeProperties = null, bool tracked = true)
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
    }
}

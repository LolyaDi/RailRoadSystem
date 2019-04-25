using System;
using System.Data.Entity;
using System.Linq;

namespace RailRoadSystem.DataAccess
{
    public class Repository : IDisposable
    {
        private DataContext _context;

        public Repository()
        {
            _context = new DataContext();
        }

        public IQueryable<TEntity> Select<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

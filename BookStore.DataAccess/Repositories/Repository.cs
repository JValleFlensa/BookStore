using BookStore.DataAccess.IRepositories;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet =  this._context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this._dbSet.Add(entity);
        }

        public IEnumerable<TEntity> GetAll(string? includeProperties = null)
        {
            IQueryable<TEntity> query = this._dbSet;
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new[] { ',' }))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter, string? includeProperties = null)
        {
            IQueryable<TEntity> query = this._dbSet;
            query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new[] { ',' }))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(TEntity entity)
        {
            this._dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.RemoveRange(entities);
        }
    }
}

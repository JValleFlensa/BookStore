using System.Linq.Expressions;

namespace BookStore.DataAccess.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter, string? includeProperties = null);
        public IEnumerable<TEntity> GetAll(string? includeProperties = null);
        public void Remove(TEntity entity);
        public void Add(TEntity entity);
        public void RemoveRange(IEnumerable<TEntity> entities);
    }
}

using System.Linq.Expressions;

namespace BookStore.DataAccess.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter);
        public IEnumerable<TEntity> GetAll();
        public void Remove(TEntity entity);
        public void Add(TEntity entity);
        public void RemoveRange(IEnumerable<TEntity> entities);
    }
}

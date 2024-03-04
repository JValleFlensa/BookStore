using BookStore.Models;

namespace BookStore.DataAccess.IRepositories
{
    public interface ICategoryRepository :IRepository<Category>
    {
        public void Update(Category category);
    }
}

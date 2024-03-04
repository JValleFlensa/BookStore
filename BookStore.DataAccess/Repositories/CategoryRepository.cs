using BookStore.DataAccess.IRepositories;
using BookStore.Models;

namespace BookStore.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context) 
        {
            this._context = context;
        }

        public void Update(Category category)
        {
            this._context.Update(category);
        }
    }
}

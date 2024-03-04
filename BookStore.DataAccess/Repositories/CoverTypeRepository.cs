using BookStore.DataAccess.IRepositories;
using BookStore.Models;

namespace BookStore.DataAccess.Repositories
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {

        private readonly ApplicationDbContext _context;
        public CoverTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public void Update(CoverType coverType)
        {
            this._context.Update(coverType);
        }
    }
}

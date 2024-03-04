using BookStore.DataAccess.IRepositories;

namespace BookStore.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public UnitOfWork(ApplicationDbContext context, ICategoryRepository categoryRepository, ICoverTypeRepository coverTypeRepository)
        {
            this._context = context;
            this.Category = categoryRepository;
            this.CoverType = coverTypeRepository;
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
}

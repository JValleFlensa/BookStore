using BookStore.DataAccess.IRepositories;

namespace BookStore.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public UnitOfWork(ApplicationDbContext context, 
            ICategoryRepository categoryRepository, 
            ICoverTypeRepository coverTypeRepository, 
            IProductRepository product)
        {
            _context = context;
            Category = categoryRepository;
            CoverType = coverTypeRepository;
            Product = product;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

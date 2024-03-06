using BookStore.DataAccess.IRepositories;
using BookStore.Models;

namespace BookStore.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public void Update(Product product)
        { 
            var productFromDb = this._context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Tittle = product.Tittle;
                productFromDb.Description = product.Description;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Author = product.Author;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price50 = product.Price50;
                productFromDb.Price100 = product.Price100;
                productFromDb.ImageURL = product.ImageURL != null ? product.ImageURL : productFromDb.ImageURL;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.CoverTypeId = product.CoverTypeId;
                this._context.Update(productFromDb);
            }           
        }
    }
}
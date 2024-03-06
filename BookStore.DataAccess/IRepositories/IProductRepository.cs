using BookStore.Models;

namespace BookStore.DataAccess.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product product);
    }
}

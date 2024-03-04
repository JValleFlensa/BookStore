using BookStore.Models;

namespace BookStore.DataAccess.IRepositories
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        public void Update(CoverType coverType);
    }
}

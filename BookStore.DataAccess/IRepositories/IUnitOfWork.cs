namespace BookStore.DataAccess.IRepositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public ICoverTypeRepository CoverType { get; }
        public void Save();
    }
}

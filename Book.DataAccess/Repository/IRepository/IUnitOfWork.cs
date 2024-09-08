namespace Book.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
	ICategoryReposetory Category { get; }
	IProductRepository Product { get; }
	void Save();
}

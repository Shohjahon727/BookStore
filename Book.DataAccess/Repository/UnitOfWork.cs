using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
namespace Book.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _db;
	public ICategoryReposetory Category { get; private set; }
	public IProductRepository Product { get; private set; }
	public UnitOfWork(AppDbContext db) 
	{
		_db = db;
		Product = new ProductRepository(_db);
		Category = new CategoryReposetory(_db);
	}

	public void Save()
	{
		_db.SaveChanges();
	}
}

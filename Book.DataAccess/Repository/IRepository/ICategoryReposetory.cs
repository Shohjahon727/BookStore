using Book.Models.Entities;

namespace Book.DataAccess.Repository.IRepository;

public interface ICategoryReposetory : IRepository<Category>
{
	void Update(Category obj);
	
}

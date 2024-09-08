using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models.Entities;
using System.Linq.Expressions;

namespace Book.DataAccess.Repository
{
    public class CategoryReposetory : Repository<Category>, ICategoryReposetory 
	{
		private readonly AppDbContext _db;
        public CategoryReposetory(AppDbContext db) : base(db) 
        {
            _db = db;
        }
       

		public void Update(Category obj)
		{
			_db.Categories.Update(obj);
		}
	}
}

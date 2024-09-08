using System.Linq.Expressions;

namespace Book.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
	// T Category
	IEnumerable<T> GetAll(string? includeProperties = null);
	T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); // parametri LINQ ifodasini oladi, bu orqali ma'lum bir obyektni qidirish mumkin. => Category category = categoryRepository.Get(c => c.Id == 1);
	void Add(T entity);
	void Update(T entity);
	void Remove(T entity);
	void RemoveRange (IEnumerable<T> entity); // bu metod bir vaqtni ozida bir nechta metodni ochirish imkonini beradi
}

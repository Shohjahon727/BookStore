using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly AppDbContext _db;
	internal readonly DbSet<T> dbSet;
	public Repository( AppDbContext db)
    {
        _db = db;
		this.dbSet = _db.Set<T>(); // _db.Categories == _dbSet
		_db.Products.Include(u => u.Category).Include(u => u.CategoryID);
	}
    public void Add(T entity)
	{
		dbSet.Add(entity);
	}

	public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
	{
		IQueryable<T> query = dbSet; // LINQ so'rovlarini kechiktirilgan (deferred execution) bajarishga imkon beradi, ya'ni so'rov faqat to'plamdan ma'lumotlar olinishi kerak bo'lganda bajariladi.
		
		query = query.Where(filter); //filter parametri Expression<Func<T, bool>> tipida bo'lib, u shart (masalan, c => c.Id == 1) ifodasini qabul qiladi.
		if (!string.IsNullOrEmpty(includeProperties))
		{
			foreach (var includeProp in includeProperties.
				Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp);
			}
		}
		return query.FirstOrDefault();
	}

	public IEnumerable<T> GetAll(string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;
		if(!string.IsNullOrEmpty(includeProperties))
		{
			foreach(var includeProp in includeProperties.
				Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp);
			}
		}
		return query.ToList();
	}

	public void Remove(T entity)
	{
		dbSet.Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entity)
	{
		dbSet.RemoveRange(entity);
	}

	public void Update(T entity)
	{
		dbSet.Update(entity);
	}
}

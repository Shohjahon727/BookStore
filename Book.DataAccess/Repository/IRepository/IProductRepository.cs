using Book.Models.Entities;

namespace Book.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}

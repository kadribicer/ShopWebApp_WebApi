using Shop.Dal.Abstract.IRepository;
using Shop.Entities.Concrete;

namespace Shop.Dal.Abstract.IRepository
{
    public interface IProductRepository : IEntityRepository<Product>
    {
    }
}
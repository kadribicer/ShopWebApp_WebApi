using Shop.Entities.Concrete;

namespace Shop.Bll.Abstract.IManager
{
    public interface IProductManager
    {
        List<Product> GetProducts(List<int> ids);
    }
}
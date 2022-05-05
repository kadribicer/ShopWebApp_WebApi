using Shop.Entities.Concrete;

namespace Shop.Bll.Abstract.IManager
{
    public interface ICategoryManager
    {
        List<Category> GetAllCategory();
    }
}
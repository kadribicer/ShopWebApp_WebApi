using Shop.Entities.Concrete;

namespace Shop.MVCWebUI.ViewModel
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public User User { get; set; }
        public List<ProductVM> ProductVMs { get; set; }
    }
}
using Shop.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Shop.Entities.Concrete
{
    public class Category : BaseEntity
    {
        #region Constructor
        public Category()
        {
            Products = new List<Product>();
        }
        #endregion

        #region Veriables
        [Display(Name = "Category")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        #endregion    
    }
}
using Shop.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Entities.Concrete
{
    public class Product : BaseEntity
    {

        [Display(Name = "Product Name")]
        public string Name { get; set; }


        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }


        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
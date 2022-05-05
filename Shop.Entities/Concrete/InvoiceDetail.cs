using Shop.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Entities.Concrete
{
    public class InvoiceDetail : BaseEntity
    {
        public string ProductName { get; set; }
        public double ProductUnitPrice { get; set; }
        public string ProductCategory { get; set; }
        public int ProductTotalQuantity { get; set; }
        public double ProductTotalPrice { get; set; }


        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
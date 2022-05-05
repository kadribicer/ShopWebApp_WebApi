using Shop.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Entities.Concrete
{
    public class Invoice : BaseEntity
    {
        #region Constructor
        public Invoice()
        {
            InvoiceDetails = new List<InvoiceDetail>();
        }
        #endregion

        #region Veriables
        public double SubTotalPrice { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public DateTime InvoiceDate { get; set; }


        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        #endregion    
    }
}
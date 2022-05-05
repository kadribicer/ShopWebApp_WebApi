using Shop.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Entities.Concrete
{
    public class User : BaseEntity
    {
        #region Constructor
        public User()
        {
            Invoices = new List<Invoice>();
        }
        #endregion

        #region Veriables
        public string UserName { get; set; }
        public string UserDetails { get; set; }
        public DateTime JoinDate { get; set; }


        [ForeignKey("UserTypeId")]
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        #endregion
    }
}
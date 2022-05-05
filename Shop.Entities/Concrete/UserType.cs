using Shop.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Shop.Entities.Concrete
{
    public class UserType : BaseEntity
    {
        #region Constructor
        public UserType()
        {
            Users = new List<User>();
        }
        #endregion

        #region Veriables

        [Display(Name = "User Type")]
        public string Role { get; set; }

        public ICollection<User> Users { get; set; }
        #endregion
    }
}
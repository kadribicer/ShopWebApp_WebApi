using Shop.Entities.Concrete;

namespace Shop.Bll.Abstract.IManager
{
    public interface IUserManager
    {
        User GetUser(string userName);
        List<User> GetAllUsers();
    }
}
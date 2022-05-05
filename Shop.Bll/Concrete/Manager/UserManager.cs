using Shop.Bll.Abstract.IManager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Entities.Concrete;

namespace Shop.Bll.Concrete.Manager
{
    public class UserManager : IUserManager
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Custom Methods
        public User GetUser(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                return null;

            return _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Status && x.UserName == userName, includeProperties: "UserType");
        }

        public List<User> GetAllUsers() => _unitOfWork.UserRepository.GetAll(x => x.Status, includeProperties: "UserType").ToList();
        #endregion
    }
}
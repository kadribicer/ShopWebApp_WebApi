using Shop.Bll.Abstract.IManager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Entities.Concrete;

namespace Shop.Bll.Concrete.Manager
{
    public class CategoryManager : ICategoryManager
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Custom Methods
        public List<Category> GetAllCategory() => _unitOfWork.CategoryRepository.GetAll(x => x.Status, includeProperties: "Products").ToList();
        #endregion
    }
}
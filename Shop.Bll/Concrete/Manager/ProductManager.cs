using Shop.Bll.Abstract.IManager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Entities.Concrete;

namespace Shop.Bll.Concrete.Manager
{
    public class ProductManager : IProductManager
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Custom Methods
        public List<Product> GetProducts(List<int> ids) => _unitOfWork.ProductRepository.GetAll(x => ids.Contains(x.Id), includeProperties: "Category").ToList();
        #endregion
    }
}
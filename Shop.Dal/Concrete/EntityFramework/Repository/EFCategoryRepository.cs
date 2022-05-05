using Shop.Dal.Abstract.IRepository;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using Shop.Entities.Concrete;

namespace Shop.Dal.Concrete.EntityFramework.Repository
{
    public class EFCategoryRepository : EFEntityRepository<Category>, ICategoryRepository
    {
        #region Veriables
        public DataContext DataContext { get { return _context; } }
        #endregion

        #region Constructor
        public EFCategoryRepository(DataContext context) : base(context) { }
        #endregion
    }
}
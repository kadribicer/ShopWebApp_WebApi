using Shop.Dal.Abstract.IRepository;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using Shop.Entities.Concrete;

namespace Shop.Dal.Concrete.EntityFramework.Repository
{
    public class EFUserRepository : EFEntityRepository<User>, IUserRepository
    {
        #region Veriables
        public DataContext DataContext { get { return _context; } }
        #endregion

        #region Constructor
        public EFUserRepository(DataContext context) : base(context) { }
        #endregion    
    }
}
using Shop.Dal.Abstract.IRepository;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using Shop.Entities.Concrete;

namespace Shop.Dal.Concrete.EntityFramework.Repository
{
    public class EFInvoiceRepository : EFEntityRepository<Invoice>, IInvoiceRepository
    {
        #region Veriables
        public DataContext DataContext { get { return _context; } }
        #endregion

        #region Constructor
        public EFInvoiceRepository(DataContext context) : base(context) { }
        #endregion    
    }
}
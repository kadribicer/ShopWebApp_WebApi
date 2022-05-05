using Shop.Dal.Abstract.IRepository;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using Shop.Dal.Concrete.EntityFramework.Repository;

namespace Shop.Dal.Concrete.EntityFramework.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        #region Veriables
        private bool _disposed;
        private readonly DataContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IInvoiceRepository InvoiceRepository { get; private set; }
        #endregion

        #region Constructor
        public EFUnitOfWork(DataContext context)
        {
            _context = context;
            CategoryRepository = new EFCategoryRepository(_context);
            ProductRepository = new EFProductRepository(_context);
            UserRepository = new EFUserRepository(_context);
            InvoiceRepository = new EFInvoiceRepository(_context);
        }
        #endregion

        #region Custom Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
        public int Save() => _context.SaveChanges();
        #endregion
    }
}
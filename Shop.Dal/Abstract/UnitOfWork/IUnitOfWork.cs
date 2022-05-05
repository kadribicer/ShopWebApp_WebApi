using Shop.Dal.Abstract.IRepository;

namespace Shop.Dal.Abstract.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        int Save();
    }
}
using Shop.Entities.Concrete;

namespace Shop.Bll.Abstract.IManager
{
    public interface IInvoiceManager
    {
        void AddInvoice(Invoice invoice);
        double GetFinalPrice(string username, List<InvoiceDetail> invoicesDetails);
        double GetSubTotalPrice(List<InvoiceDetail> invoicesDetails);
        Invoice GetLastInvoice(int userId);

        #region Web API Requests
        Task<List<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceById(int id); 
        #endregion
    }
}
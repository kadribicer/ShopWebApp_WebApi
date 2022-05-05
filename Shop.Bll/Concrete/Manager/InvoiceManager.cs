using Shop.Bll.Abstract.IManager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Entities.Concrete;

namespace Shop.Bll.Concrete.Manager
{
    public class InvoiceManager : IInvoiceManager
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public InvoiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Custom Methods
        public void AddInvoice(Invoice invoice)
        {
            _unitOfWork.InvoiceRepository.Add(invoice);
            _unitOfWork.Save();
        }
        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _unitOfWork.InvoiceRepository.GetAllAsync(x => x.Status, includeProperties: "InvoiceDetails");
        }
        public async Task<Invoice> GetInvoiceById(int id) => await _unitOfWork.InvoiceRepository.GetAsync(x => x.Id == id, includeProperties: "InvoiceDetails");

        public Invoice GetLastInvoice(int userId) => _unitOfWork.InvoiceRepository.GetFirstOrDefault(x => x.UserId == userId, includeProperties: "InvoiceDetails", orderBy: q => q.OrderByDescending(d => d.Id));
        public double GetFinalPrice(string username, List<InvoiceDetail> invoicesDetails)
        {
            if (invoicesDetails.Count == 0 || String.IsNullOrEmpty(username))
                return 0;

            double groceriesPrice = 0;
            double otherPrice = 0;

            invoicesDetails.ForEach(product =>
            {
                groceriesPrice += product.ProductCategory == "Groceries" ? product.ProductTotalPrice : 0;
                otherPrice += product.ProductCategory != "Groceries" ? product.ProductTotalPrice : 0;
            });

            return GetDiscount(_unitOfWork.UserRepository.GetFirstOrDefault(x => x.UserName == username, includeProperties: "UserType"), groceriesPrice, otherPrice);
        }
        public double GetSubTotalPrice(List<InvoiceDetail> invoicesDetails)
        {
            double subTotalPrice = 0;
            invoicesDetails.ForEach(product => subTotalPrice += product.ProductTotalPrice);
            return subTotalPrice;
        }
        public static double GetDiscount(User user, double groceriesPrice, double otherPrice)
        {
            double discountedPrice = 0;
            switch (user.UserType.Role)
            {
                case "Employee":
                    discountedPrice = GetPercentageDiscount(otherPrice, 30);
                    break;
                case "Affiliate":
                    discountedPrice = GetPercentageDiscount(otherPrice, 10);
                    break;
                case "Customer":
                    DateTime today = DateTime.Now, joinDate = user.JoinDate;
                    TimeSpan ts = joinDate - today;
                    if (Math.Abs(ts.Days) > 730)
                        discountedPrice = GetPercentageDiscount(otherPrice, 5);
                    else
                        discountedPrice = otherPrice;
                    break;
            }

            return FiveDollarDiscountForEveryHundredDollars(discountedPrice + groceriesPrice);
        }
        public static double GetPercentageDiscount(double otherPrice, double discountPercentage) => otherPrice - (otherPrice * discountPercentage / 100);
        public static double FiveDollarDiscountForEveryHundredDollars(double totalPrice) => totalPrice > 100 ? totalPrice - (Math.Floor(totalPrice / 100) * 5) : totalPrice;
        #endregion
    }
}
using Microsoft.AspNetCore.Mvc;
using Shop.Bll.Abstract.IManager;
using Shop.Entities.Concrete;
using Shop.MVCWebUI.ViewModel;

namespace Shop.MVCWebUI.Controllers
{
    public class HomeController : Controller
    {
        #region Veriables
        private readonly ICategoryManager _categoryManager;
        private readonly IUserManager _userManager;
        private readonly IInvoiceManager _invoiveManager;
        #endregion

        #region Constructor
        public HomeController(ICategoryManager categoryManager, IUserManager userManager, IInvoiceManager invoiveManager)
        {
            _categoryManager = categoryManager;
            _userManager = userManager;
            _invoiveManager = invoiveManager;
        }
        #endregion

        #region Actions
        [HttpGet]
        public IActionResult Login() => View(_userManager.GetAllUsers());

        [HttpGet]
        [Route("shop/{username?}")]
        public IActionResult Index(string username)
        {
            if (String.IsNullOrEmpty(username))
                return Redirect("/");

            ViewBag.Data = "Shop Discount";
            return View(new HomeVM
            {
                Categories = _categoryManager.GetAllCategory(),
                User = _userManager.GetUser(username)
            });
        }

        [HttpPost]
        [Route("shop/{username?}")]
        public IActionResult Index(string username, List<ProductVM> ProductVMs)
        {
            try
            {
                var model = ProductVMs.Where(x => x.ProductQuantity > 0).ToList();
                if (model.Count == 0)
                {
                    ViewBag.Data = "Shop Discount";
                    ViewBag.Status = "Required";

                    return View(new HomeVM
                    {
                        Categories = _categoryManager.GetAllCategory(),
                        User = _userManager.GetUser(username)
                    });
                }
                else
                {
                    var userModel = _userManager.GetUser(username);
                    List<InvoiceDetail> invoicesDetails = new();
                    ProductVMs.ForEach(product =>
                    {
                        if (product.ProductQuantity > 0)
                        {
                            InvoiceDetail invoiceDetailModel = new()
                            {
                                ProductName = product.ProductName,
                                ProductUnitPrice = product.ProductUnitPrice,
                                ProductCategory = product.ProductCategory == 1 ? "Groceries" : "Electronic",
                                ProductTotalQuantity = product.ProductQuantity,
                                ProductTotalPrice = product.ProductQuantity * product.ProductUnitPrice,
                                Status = true,
                            };
                            invoicesDetails.Add(invoiceDetailModel);
                        }
                    });
                    Invoice invoice = new()
                    {
                        InvoiceDate = DateTime.Now,
                        InvoiceDetails = invoicesDetails,
                        SubTotalPrice = _invoiveManager.GetSubTotalPrice(invoicesDetails),
                        TotalPrice = _invoiveManager.GetFinalPrice(username, invoicesDetails),
                        UserId = userModel.Id,
                        Status = true,
                    };

                    invoice.DiscountPrice = invoice.SubTotalPrice - invoice.TotalPrice;
                    _invoiveManager.AddInvoice(invoice);
                    return Redirect("/invoice/" + username);
                }
            }
            catch
            {
                ViewBag.Data = "Shop Discount";
                ViewBag.Status = "Exception";

                return View(new HomeVM
                {
                    Categories = _categoryManager.GetAllCategory(),
                    User = _userManager.GetUser(username)
                });
            }
        }

        [HttpGet]
        [Route("invoice/{username?}")]
        public IActionResult InvoicePage(string username)
        {
            if (String.IsNullOrEmpty(username))
                return Redirect("/");

            ViewBag.Data = "Invoice";
            var userModel = _userManager.GetUser(username);
            return View(new InvoiceVM
            {
                User = userModel,
                Invoice = _invoiveManager.GetLastInvoice(userModel.Id)
            });
        }
        #endregion
    }
}
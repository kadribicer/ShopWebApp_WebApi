using Microsoft.AspNetCore.Mvc;
using Moq;
using Shop.Bll.Abstract.IManager;
using Shop.Entities.Concrete;
using Shop.MVCWebUI.Controllers;
using Shop.MVCWebUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shop.MVCWebUI.Tests
{
    public class HomeControllerTests
    {
        #region Veriables
        private readonly Mock<ICategoryManager> _categoryManager;
        private readonly Mock<IUserManager> _userManager;
        private readonly Mock<IInvoiceManager> _invoiceManager;
        private readonly HomeController _homeController;
        private readonly List<User> users;
        private readonly List<Category> categories;
        private readonly Invoice invoice;
        private readonly List<InvoiceDetail> invoicesDetail;
        private readonly List<ProductVM> productVMs;
        #endregion

        #region Constructor
        public HomeControllerTests()
        {
            _categoryManager = new Mock<ICategoryManager>();
            _userManager = new Mock<IUserManager>();
            _invoiceManager = new Mock<IInvoiceManager>(MockBehavior.Loose);
            _homeController = new HomeController(_categoryManager.Object, _userManager.Object, _invoiceManager.Object);
            users = new List<User>()
            {
                new User {
                    Id = 1,
                    UserName ="employee",
                    JoinDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                    //UserTypeId = 1,
                    Status = true,
                },
                new User {
                    Id = 1,
                    UserName ="affiliate",
                    JoinDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                    //UserTypeId = 2,
                    Status = true,
                },
                new User {
                    Id = 1,
                    UserName ="customer",
                    JoinDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                    //UserTypeId = 3,
                    Status = true,
                },
                new User {
                    Id = 1,
                    UserName ="old_customer",
                    JoinDate = Convert.ToDateTime("2019-04-28 00:00:00.0000000"),
                    //UserTypeId = 3,
                    Status = true,
                },
            };
            categories = new List<Category>()
            {
                new Category {
                    Id = 1,
                    Name ="Groceries",
                    Status = true,
                },
                new Category {
                    Id = 2,
                    Name ="Electronic",
                    Status = true,
                },
            };
            invoicesDetail = new List<InvoiceDetail>()
            {
                new InvoiceDetail
                {
                    ProductName = "Strawberry",
                    ProductUnitPrice = 3,
                    ProductCategory = "Groceries",
                    ProductTotalQuantity = 12,
                    ProductTotalPrice = 36,
                    InvoiceId = 1,
                },
                new InvoiceDetail
                {
                    ProductName = "Notebook",
                    ProductUnitPrice = 350,
                    ProductCategory = "Electronic",
                    ProductTotalQuantity = 3,
                    ProductTotalPrice = 1050,
                    InvoiceId = 1,
                },
                new InvoiceDetail
                {
                    ProductName = "Tablet",
                    ProductUnitPrice = 180,
                    ProductCategory = "Electronic",
                    ProductTotalQuantity = 5,
                    ProductTotalPrice = 900,
                    InvoiceId = 1,
                },
            };
            invoice = new Invoice
            {
                Id = 1,
                SubTotalPrice = 15,
                TotalPrice = 30,
                DiscountPrice = 5,
                InvoiceDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                UserId = 1,
                InvoiceDetails = invoicesDetail,
            };
            productVMs = new List<ProductVM>()
            {
                new ProductVM
                {
                    ProductId = 1,
                    ProductName = "Strawberry",
                    ProductQuantity = 0,
                    ProductUnitPrice = 3,
                    ProductCategory = 1,
                },
                new ProductVM
                {
                    ProductId = 2,
                    ProductName = "Notebook",
                    ProductQuantity = 5,
                    ProductUnitPrice = 350,
                    ProductCategory = 2,
                },
                new ProductVM
                {
                    ProductId = 3,
                    ProductName = "Tablet",
                    ProductQuantity = 12,
                    ProductUnitPrice = 180,
                    ProductCategory = 2,
                },
            };
        }
        #endregion

        #region Test Methods
        [Fact]
        public void Login_ActionExecute_Return_View()
        {
            var result = _homeController.Login();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Login_ActionExecute_Return_UserList()
        {
            _userManager.Setup(x => x.GetAllUsers()).Returns(users);
            var result = _homeController.Login();
            var viewResult = Assert.IsType<ViewResult>(result);
            var userList = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            Assert.Equal<int>(4, userList.Count());
        }

        [Fact]
        public void Index_ActionExecute_Return_View()
        {
            var result = _homeController.Index(users[0].UserName);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ActionExecute_Return_HomeVM()
        {
            _categoryManager.Setup(x => x.GetAllCategory()).Returns(categories);
            _userManager.Setup(X => X.GetUser("employee")).Returns(users[0]);

            var homeVMTest = new HomeVM
            {
                Categories = categories,
                User = users[0]
            };

            var result = _homeController.Index("employee");
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<HomeVM>(homeVMTest);
        }

        [Fact]
        public void Index_ActionExecute_ProductVM_IsNull_Return_HomeVM()
        {
            _categoryManager.Setup(x => x.GetAllCategory()).Returns(categories);
            _userManager.Setup(X => X.GetUser("employee")).Returns(users[0]);

            var homeVMTest = new HomeVM
            {
                Categories = categories,
                User = users[0]
            };

            var result = _homeController.Index("employee", null);
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<HomeVM>(homeVMTest);
        }

        [Fact]
        public void Index_ActionExecute_Create_Invoice_Return_View()
        {
            _userManager.Setup(X => X.GetUser("employee")).Returns(users[0]);
            _invoiceManager.Setup(x => x.GetLastInvoice(users[0].Id)).Returns(invoice);

            var result = _homeController.Index("employee", productVMs);
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/invoice/" + users[0].UserName, redirect.Url);
        }

        [Fact]
        public void InvoicePage_ActionExecute_Username_IsNull_Redirect_MainPage()
        {
            var result = _homeController.InvoicePage(null);
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirect.Url);
        }

        [Fact]
        public void InvoicePage_ActionExecute_Return_InvoiceVM()
        {
            _userManager.Setup(X => X.GetUser("employee")).Returns(users[0]);
            _invoiceManager.Setup(x => x.GetLastInvoice(users[0].Id)).Returns(invoice);

            var invoiceVmTest = new InvoiceVM
            {
                User = users[0],
                Invoice = invoice
            };

            var result = _homeController.InvoicePage(users[0].UserName);
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<InvoiceVM>(invoiceVmTest);
        }
        #endregion
    }
}
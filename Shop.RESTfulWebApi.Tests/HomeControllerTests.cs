using Microsoft.AspNetCore.Mvc;
using Moq;
using Shop.Bll.Abstract.IManager;
using Shop.Dal.Abstract.IRepository;
using Shop.Entities.Concrete;
using Shop.RESTfulWebApi.Controller;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Shop.RESTfulWebApi.Tests
{
    public class HomeControllerTests
    {
        #region Veriables
        private readonly Mock<IInvoiceManager> _invoiceManager;
        private readonly HomeController _homeController;
        private readonly List<Invoice> invoices;
        private readonly Invoice invoice;
        private readonly List<InvoiceDetail> invoicesDetail;
        #endregion

        #region Constructor
        public HomeControllerTests()
        {
            _invoiceManager = new Mock<IInvoiceManager>(MockBehavior.Loose);
            _homeController = new HomeController(_invoiceManager.Object);
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
            invoices = new List<Invoice>();
        }
        #endregion

        #region Test Methods
        [Fact]
        public async Task GetInvoices_NoContent_Test()
        {
            _invoiceManager.Setup(x => x.GetAllInvoicesAsync()).ReturnsAsync(invoices);
            var result = await _homeController.GetInvoices();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetInvoices_Ok_Test()
        {
            invoices.Add(invoice);
            _invoiceManager.Setup(x => x.GetAllInvoicesAsync()).ReturnsAsync(invoices);
            var result = await _homeController.GetInvoices();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetInvoicesById_Content_IsNull()
        {
            _invoiceManager.Setup(x => x.GetInvoiceById(2)).ReturnsAsync(invoice);
            var result = await _homeController.GetInvoicesById(2);
            Assert.IsType<OkObjectResult>(result);
            var invoiceResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Empty(invoiceResult.ContentTypes);
        }

        [Fact]
        public async Task GetInvoicesById_Ok_Test()
        {
            _invoiceManager.Setup(x => x.GetInvoiceById(1)).ReturnsAsync(invoice);
            var result = await _homeController.GetInvoicesById(1);
            Assert.IsType<OkObjectResult>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var invoiceResult = Assert.IsAssignableFrom<Invoice>(objectResult.Value);
            Assert.Equal(1, invoiceResult.Id);
        }
        #endregion
    }
}
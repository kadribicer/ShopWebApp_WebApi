using Moq;
using Shop.Bll.Concrete.Manager;
using Shop.Dal.Abstract.UnitOfWork;
using Shop.Entities.Concrete;
using System;
using System.Collections.Generic;
using Xunit;

namespace Shop.Bll.Tests
{
    public class InvoiceManagerTests
    {
        #region Veriables
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly InvoiceManager _invoiceManager;
        private readonly Invoice invoice;
        private List<InvoiceDetail> invoicesDetails;
        #endregion

        #region Constructor
        public InvoiceManagerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _invoiceManager = new InvoiceManager(_unitOfWork.Object);
            invoice = new Invoice
            {
                SubTotalPrice = 840,
                TotalPrice = 567.5,
                DiscountPrice = 272.5,
                InvoiceDate = Convert.ToDateTime("2022-05-02 04:20:47.7829779"),
            };
            invoicesDetails = new List<InvoiceDetail>
            {
                new InvoiceDetail
                {
                    ProductName = "Strawberry",
                    ProductUnitPrice = 3,
                    ProductCategory = "Groceries",
                    ProductTotalQuantity = 5,
                    ProductTotalPrice = 15,
                    Invoice = invoice
                },
                new InvoiceDetail
                {
                    ProductName = "Phone",
                    ProductUnitPrice = 275,
                    ProductCategory = "Electronic",
                    ProductTotalQuantity = 3,
                    ProductTotalPrice = 825,
                    Invoice = invoice
                },
            };
        }
        #endregion

        #region Test Methods
        [Fact]
        public void GetFinalPrice_InvoiceDetail_Or_Username_IsNull()
        {
            invoicesDetails = new List<InvoiceDetail>();
            var result = _invoiceManager.GetFinalPrice(username: null, invoicesDetails);
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetSubTotalPrice_Tests()
        {
            var result = _invoiceManager.GetSubTotalPrice(invoicesDetails);
            Assert.Equal(840, result);
        }

        [Fact]
        public static void GetDiscount_Employee_Final_Price()
        {
            var employe = new User
            {
                Id = 1,
                UserName = "employee",
                JoinDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                //UserTypeId = 1,
                UserType = new UserType
                {
                    Id = 1,
                    Role = "Employee",
                    Status = true,
                },
                Status = true,
            };

            var result = InvoiceManager.GetDiscount(employe, 845, 2300);
            Assert.Equal(2335, result);
        }

        [Fact]
        public static void GetDiscount_Affiliate_Final_Price()
        {
            var affiliate = new User
            {
                Id = 1,
                UserName = "affiliate",
                JoinDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                //UserTypeId = 2,
                UserType = new UserType
                {
                    Id = 2,
                    Role = "Affiliate",
                    Status = true,
                },
                Status = true,
            };

            var result = InvoiceManager.GetDiscount(affiliate, 845, 2300);
            Assert.Equal(2770, result);
        }

        [Fact]
        public static void GetDiscount_Customer_Final_Price()
        {
            var customer = new User
            {
                Id = 1,
                UserName = "customer",
                JoinDate = Convert.ToDateTime("2021-04-28 00:00:00.0000000"),
                //UserTypeId = 3,
                UserType = new UserType
                {
                    Id = 3,
                    Role = "Customer",
                    Status = true,
                },
                Status = true,
            };

            var result = InvoiceManager.GetDiscount(customer, 845, 2300);
            Assert.Equal(2990, result);
        }

        [Fact]
        public static void GetDiscount_Old_Customer_Final_Price()
        {
            var oldCustomer = new User
            {
                Id = 1,
                UserName = "old_customer",
                JoinDate = Convert.ToDateTime("2019-04-28 00:00:00.0000000"),
                //UserTypeId = 3,
                UserType = new UserType
                {
                    Id = 3,
                    Role = "Customer",
                    Status = true,
                },
                Status = true,
            };

            var result = InvoiceManager.GetDiscount(oldCustomer, 845, 2300);
            Assert.Equal(2880, result);
        }

        [Fact]
        public static void GetPercentageDiscount_Tests()
        {
            var result = InvoiceManager.GetPercentageDiscount(1850, 30);
            Assert.Equal(1295, result);
        }

        [Fact]
        public static void Five_Dollar_Discount_For_Every_Hundred_Dollars_Tests()
        {
            var result = InvoiceManager.FiveDollarDiscountForEveryHundredDollars(1850);
            Assert.Equal(1760, result);
        }
        #endregion
    }
}
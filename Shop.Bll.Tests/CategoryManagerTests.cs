using Moq;
using Shop.Bll.Abstract.IManager;
using Shop.Entities.Concrete;
using System.Collections.Generic;
using Xunit;

namespace Shop.Bll.Tests
{
    public class CategoryManagerTests
    {
        #region Veriables
        private readonly Mock<ICategoryManager> _categoryManager;
        private List<Category> categories;
        #endregion

        #region Constructor
        public CategoryManagerTests()
        {
            _categoryManager = new Mock<ICategoryManager>();
            categories = new List<Category>();
        }
        #endregion

        #region Test Methods
        [Fact]
        public void GetAllCategory_Return_IsNull()
        {
            categories.Clear();
            _categoryManager.Setup(x => x.GetAllCategory()).Returns(categories);
            var categoryList = Assert.IsAssignableFrom<List<Category>>(categories);
            Assert.Empty(categoryList);
        }

        [Fact]
        public void GetAllCategory_Return_Categories()
        {
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

            _categoryManager.Setup(x => x.GetAllCategory()).Returns(categories);
            Assert.IsAssignableFrom<List<Category>>(categories);
            Assert.Equal<int>(2, categories.Count);
        }
        #endregion
    }
}
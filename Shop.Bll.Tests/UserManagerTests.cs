using Moq;
using Shop.Bll.Abstract.IManager;
using Shop.Entities.Concrete;
using System;
using System.Collections.Generic;
using Xunit;

namespace Shop.Bll.Tests
{
    public class UserManagerTests
    {
        #region Veriables
        private readonly Mock<IUserManager> _userManager;
        private List<User> users;
        #endregion

        #region Constructor
        public UserManagerTests()
        {
            _userManager = new Mock<IUserManager>();
            users = new List<User>();
        }
        #endregion

        #region Test Methods
        [Fact]
        public void GetAllCategory_Return_IsNull()
        {
            users.Clear();
            _userManager.Setup(x => x.GetAllUsers()).Returns(users);
            var userList = Assert.IsAssignableFrom<List<User>>(users);
            Assert.Empty(userList);
        }

        [Fact]
        public void GetAllCategory_Return_Categories()
        {
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
                    //UserTypeId = 1,
                    Status = true,
                },
                new User {
                    Id = 1,
                    UserName ="old_customer",
                    JoinDate = Convert.ToDateTime("2019-04-28 00:00:00.0000000"),
                    //UserTypeId = 1,
                    Status = true,
                },
            };

            _userManager.Setup(x => x.GetAllUsers()).Returns(users);
            Assert.IsAssignableFrom<List<User>>(users);
            Assert.Equal<int>(4, users.Count);
        }
        #endregion
    }
}
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieNetData;

namespace MovieNetDataTest.DAO
{
    /// <summary>
    /// Unit tests for UserDao
    /// </summary>
    [TestClass]
    public class UserDaoTest
    {
        private static MovieNetData.ViewModel.ServiceFacade serviceFacade = null;

        private static readonly int NB_USERS_TOTAL = 3;

        private static readonly string FIND_USER_BY_USERNAME = "aleparedes00";

        [TestInitialize]
        public void Init()
        {
            serviceFacade = MovieNetData.ViewModel.ServiceFacade.Instance;
        }

        [TestMethod]
        public void TestFindAllUsers()
        {
            List<User> allUsers = serviceFacade.UserDao.FindAllUsers();

            if (allUsers.Count > 0)
            {
                Assert.AreEqual(NB_USERS_TOTAL, allUsers.Count);
            }
            else Assert.Fail("No user was found.");
        }

        [TestMethod]
        public void TestFindUserByCriteria()
        {
            User userByUsername = serviceFacade.UserDao.FindUserByUsername(FIND_USER_BY_USERNAME);

            Assert.IsNotNull(userByUsername);
        }

        [TestMethod]
        public void TestCRUDUser()
        {
            // Create
            User userCRUD = new User("test", "pwd");
            userCRUD = serviceFacade.UserDao.CreateUser(userCRUD);
            Assert.IsNotNull(userCRUD);
            Assert.IsNotNull(userCRUD.Id);
            Assert.IsNotNull(serviceFacade.UserDao.FindUserById(userCRUD.Id));

            // Update
            userCRUD.Username = "test2";
            userCRUD.Password = "pwd2";
            userCRUD = serviceFacade.UserDao.UpdateUser(userCRUD);
            Assert.IsNotNull(userCRUD);
            Assert.AreEqual("test2", userCRUD.Username);
            Assert.AreEqual("pwd2", userCRUD.Password);

            // Delete
            Assert.IsTrue(serviceFacade.UserDao.DeleteUser(userCRUD));
            Assert.IsNull(serviceFacade.UserDao.FindUserById(userCRUD.Id));
            List<User> finalUsers = serviceFacade.UserDao.FindAllUsers();
            if (finalUsers != null)
            {
                Assert.AreEqual(NB_USERS_TOTAL, finalUsers.Count);
            }
            else Assert.Fail();
        }
    }
}

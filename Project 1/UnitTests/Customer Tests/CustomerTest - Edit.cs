using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data_Layer.Data_Objects;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using UnitTests.Resources;

namespace UnitTests.CustomerTests
{
    [TestClass]
    public class CustomerTest_Edit
    {
        [TestMethod]
        public void TestEditGet_ExistingCustomer()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

            tController = new CustomersController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserB"),
            };

            #endregion

            #region ACT

            var tResult = tController.Edit(10) as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as Customer;
            Assert.AreEqual(tValue.FirstName, "Mary");

            #endregion
        }

        [TestMethod]
        public void TestEditGet_ExistingCustomer_InvalidUser()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

            tController = new CustomersController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Edit(10) as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as Customer;
            Assert.AreNotEqual(tValue.FirstName, "Mary");

            #endregion
        }

        [TestMethod]
        public void TestEditGet_NewCustomer()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

            tController = new CustomersController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("User"),
            };

            #endregion

            #region ACT

            var tResult = tController.Edit(1);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");

            #endregion
        }

        [TestMethod]
        public void TestEditPost_ExistingCustomer_ValidID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;
            Customer tCustomer = new Customer()
            {
                FirstName = "Mary",
                ID = 1,
                UserIdentity = "UserB"
            };

            tController = new CustomersController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserB"),
            };

            #endregion

            #region ACT

            var tResult = tController.Edit(1, tCustomer);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestEditPost_ExistingCustomer_InvalidID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;
            Customer tCustomer = new Customer()
            {
                FirstName = "Mary",
                ID = 1,
                UserIdentity = "UserB"
            };

            tController = new CustomersController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserB"),
            };

            #endregion

            #region ACT

            var tResult = tController.Edit(0, tCustomer);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is UnauthorizedResult);

            #endregion
        }
    }
}
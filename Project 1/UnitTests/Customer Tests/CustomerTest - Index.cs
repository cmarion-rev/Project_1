using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data_Layer.View_Models;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using UnitTests.Resources;

namespace UnitTests.CustomerTests
{
    [TestClass]
    public class CustomerTest_Index
    {
        [TestMethod]
        public void TestIndexGet_NewCustomer()
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

            var tResult = tController.Index();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");

            #endregion
        }

        [TestMethod]
        public void TestIndexGet_ExistingCustomer()
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

            var tResult = tController.Index() as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as CustomerAccountsVM;
            Assert.AreEqual(tValue.Customer.FirstName, "John");
            Assert.AreEqual(tValue.Accounts.Count, 2);

            #endregion
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data_Layer.Data_Objects;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using UnitTests.Resources;

namespace UnitTests.CustomerTests
{
    [TestClass]
    public class CustomerTest_Details
    {
        [TestMethod]
        public void TestDetailsGet_ExistingCustomer()
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

            var tResult = tController.Details(null) as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as Customer;
            Assert.AreEqual(tValue.FirstName, "John");

            #endregion
        }

        [TestMethod]
        public void TestDetailsGet_NewCustomer()
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

            var tResult = tController.Details(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");

            #endregion
        }
    }
}
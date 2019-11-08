using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using UnitTests.Resources;

namespace UnitTests.AccountTests
{
    [TestClass]
    public class AccountTest_Index
    {
        [TestMethod]
        public void TestIndex()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("User"),
            };

            #endregion

            #region ACT

            var tResult = tController.Index();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }
    }
}
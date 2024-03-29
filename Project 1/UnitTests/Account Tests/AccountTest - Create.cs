using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using UnitTests.Resources;
using Data_Layer.Data_Objects;

namespace UnitTests.AccountTests
{
    [TestClass]
    public class AccountTest_Create
    {       
        [TestMethod]
        public void TestCreateGet()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Create();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).ViewData["AccountTypeID"] as SelectList).Where(t=>t.Value == "0").FirstOrDefault().Text, "Checking");

            #endregion
        }

        [TestMethod]
        public void TestCreatePost_InvalidData()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = new Account()
            {
                AccountBalance = -100.0,
            };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Create(tAccount);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as Account).AccountBalance, 0);

            #endregion
        }

        [TestMethod]
        public void TestCreatePost_ValidData()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = new Account()
            {
                AccountBalance = 100.0,
            };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Create(tAccount);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Details");
            Assert.AreEqual((tResult as RedirectToActionResult).RouteValues["id"], 4);

            #endregion
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data_Layer.View_Models;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using UnitTests.Resources;
using Data_Layer.Data_Objects;

namespace UnitTests.AccountTests
{
    [TestClass]
    public class AccountTest_Transfer
    {
        [TestMethod]
        public void TestTransferGet_NoValidTransfer()
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

            var tResult = tController.Transfer();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransferGet_ValidTransfer()
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

            var tResult = tController.Transfer();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransferVM).Amount, 0.0);
            Assert.IsTrue(((tResult as ViewResult).Model as AccountTransferVM).SourceAccounts.Count > 0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransferVM).SourceID, 0);
            Assert.IsTrue(((tResult as ViewResult).Model as AccountTransferVM).DestinationAccounts.Count > 0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransferVM).DestinationID, 0);

            #endregion
        }

        [TestMethod]
        public void TestTransferPost_UnauthorizedSourceAccountOwner()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            AccountTransferVM tVM = new AccountTransferVM()
            {
                DestinationID = 1,
                SourceID = 3,
                Amount = 1.0,
            };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transfer(tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransferPost_UnauthorizedDestinationAccountOwner()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            AccountTransferVM tVM = new AccountTransferVM()
            {
                DestinationID = 2,
                SourceID = 0,
                Amount = 1.0,
            };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transfer(tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransferPost_SameAccountSelection()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            AccountTransferVM tVM = new AccountTransferVM()
            {
                DestinationID = 0,
                SourceID = 0,
                Amount = 1.0,
            };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transfer(tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.IsNotNull((tResult as ViewResult).ViewData["ErrorMessage"]);

            #endregion
        }

        [TestMethod]
        public void TestTransferPost_OverdraftError()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            AccountTransferVM tVM = new AccountTransferVM()
            {
                DestinationID = 1,
                SourceID = 0,
                Amount = 5000.0,
            };
            Account tData1 = tRepo.GetAccountInformation(0, 0);
            Account tData2 = tRepo.GetAccountInformation(0, 1);

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transfer(tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.IsNotNull((tResult as ViewResult).ViewData["ErrorMessage"]);
            Assert.IsTrue((tResult as ViewResult).ViewData["ErrorMessage"].ToString().Contains("Overdraft"));
            Assert.AreEqual(tData1.AccountBalance, 1000.0);
            Assert.AreEqual(tData2.AccountBalance, 500.0);

            #endregion
        }

        [TestMethod]
        public void TestTransferPost_Valid()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            AccountTransferVM tVM = new AccountTransferVM()
            {
                DestinationID = 1,
                SourceID = 0,
                Amount = 500.0,
            };
            Account tData1 = tRepo.GetAccountInformation(0, 0);
            Account tData2 = tRepo.GetAccountInformation(0, 1);

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transfer(tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");
            Assert.AreEqual(tData1.AccountBalance, 500.0);
            Assert.AreEqual(tData2.AccountBalance, 1000.0);

            #endregion
        }
    }
}
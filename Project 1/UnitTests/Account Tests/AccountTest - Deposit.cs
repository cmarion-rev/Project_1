using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System;
using System.Linq;
using System.Web;
using System.Security.Claims;

using Data_Layer.Data_Objects;
using Data_Layer.View_Models;
using Web_Interface.Controllers;
using UnitTests.Repositories;
using Data_Layer.Resources;
using UnitTests.Resources;

namespace UnitTests
{
    [TestClass]
    public class AccountTest_Deposit
    {
        [TestMethod]
        public void TestDepositGet_Null()
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

            var tResult = tController.Deposit(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestDepositGet_UnautorizedAccountOwner()
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

            var tResult = tController.Deposit(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestDepositGet_NonDepositAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserB"),
            };

            #endregion

            #region ACT

            var tResult = tController.Deposit(2);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestDepositGet_ValidAccount()
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

            var tResult = tController.Deposit(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.ID, 0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Amount, 0.0);

            #endregion
        }

        [TestMethod]
        public void TestDepositPost_MismatchAccountID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Deposit(100, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestDepositPost_UnauthorizeAccountUser()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("User"),
            };

            #endregion

            #region ACT

            var tResult = tController.Deposit(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestDepositPost_NonDepositAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 2);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserB"),
            };

            #endregion

            #region ACT

            var tResult = tController.Deposit(2, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestDepositPost_Valid()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Deposit(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }
    }
}
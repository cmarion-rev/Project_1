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
    public class AccountTest_Transactions
    {
        #region TRANSACTIONS

        [TestMethod]
        public void TestTransactionGet_Null()
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

            var tResult = tController.Transactions(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransactionsGet_NewUserInvalid()
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

            var tResult = tController.Transactions(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestTransactionsGet_UnauthorizedAccountOwner()
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

            var tResult = tController.Transactions(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransactionsGet_Valid()
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

            var tResult = tController.Transactions(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as CustomerAccountTransactionsVM).Customer.ID, 0);
            Assert.AreEqual(((tResult as ViewResult).Model as CustomerAccountTransactionsVM).Account.ID, 0);
            Assert.IsTrue(((tResult as ViewResult).Model as CustomerAccountTransactionsVM).AccountTransactions.Count > 0);

            #endregion
        }

        [TestMethod]
        public void TestTransactionPost_NullID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            CustomerAccountTransactionsVM tVM = tRepo.GetAllTransactions(0, 0);

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transactions(null, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransactionPost_NullAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            CustomerAccountTransactionsVM tVM = tRepo.GetAllTransactions(0, 0);
            tVM.Account = null;

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transactions(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransactionPost_MismatchAccountID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            CustomerAccountTransactionsVM tVM = tRepo.GetAllTransactions(0, 0);

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transactions(100, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransactionPost_NewUserInvalid()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            CustomerAccountTransactionsVM tVM = tRepo.GetAllTransactions(0, 0);

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("User"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transactions(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestTransactionPost_UnauthorizedAccountOwner()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            CustomerAccountTransactionsVM tVM = tRepo.GetAllTransactions(0, 0);

            tController = new AccountsController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("UserB"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transactions(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestTransactionPost_Valid()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            CustomerAccountTransactionsVM tVM = tRepo.GetAllTransactions(0, 0);

            tController = new AccountsController(tRepo)
            {
                ControllerContext =  UtilityFunctions.GenerateMockControllerContext("UserA"),
            };

            #endregion

            #region ACT

            var tResult = tController.Transactions(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as CustomerAccountTransactionsVM).Customer.ID, 0);
            Assert.AreEqual(((tResult as ViewResult).Model as CustomerAccountTransactionsVM).Account.ID, 0);
            Assert.IsTrue(((tResult as ViewResult).Model as CustomerAccountTransactionsVM).AccountTransactions.Count > 0);

            #endregion
        }

       
        #endregion
    }
}
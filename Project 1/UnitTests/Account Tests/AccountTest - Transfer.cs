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
    }
}
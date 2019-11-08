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
    public class AccountTest
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
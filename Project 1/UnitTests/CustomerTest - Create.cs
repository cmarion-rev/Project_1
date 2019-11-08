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
using UnitTests.Resources;

namespace UnitTests
{
    [TestClass]
    public class CustomerTest_Create
    {
        [TestMethod]
        public void TestCreateViewBag()
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

            var tResult = tController.Create() as ViewResult;

            #endregion

            #region ASSERT

            var tList = tResult.ViewData["StateID"] as SelectList;

            var tItem = tList.Where(a => a.Value == "0").FirstOrDefault().Text;

            Assert.AreEqual(tItem, "FL");

            #endregion
        }

        [TestMethod]
        public void TestCreateViewBag_Invalid()
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

            var tResult = tController.Create() as ViewResult;

            #endregion

            #region ASSERT

            var tList = tResult.ViewData["StateID"] as SelectList;

            var tItem = tList.Where(a => a.Value == "1").FirstOrDefault().Text;

            Assert.AreNotEqual(tItem, "FL");

            #endregion
        }

        [TestMethod]
        public void TestCreatePost_NewCustomer()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

            Customer newCustomer = new Customer()
            {
                FirstName = "Me",
                LastName = "Guy",
                StateID = 0
            };

            tController = new CustomersController(tRepo)
            {
                ControllerContext = UtilityFunctions.GenerateMockControllerContext("User"),
            };

            #endregion

            #region ACT

            var tResult = tController.Create(newCustomer);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);

            #endregion
        }

        [TestMethod]
        public void TestCreateGet_ExistingCustomer()
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

            var tResult = tController.Create();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }
    }
}
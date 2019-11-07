using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

using NSubstitute;
using System;
using System.Security.Claims;
using UnitTests.Repositories;
using Web_Interface.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data_Layer.Data_Objects;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestCreateViewBag()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var urlHelperFactory = new Mock<ITempDataDictionaryFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(urlHelperFactory.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

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

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var urlHelperFactory = new Mock<ITempDataDictionaryFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(urlHelperFactory.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

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
    }
}
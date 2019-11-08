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

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

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

        [TestMethod]
        public void TestDetails_Null()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Details(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is NotFoundResult);

            #endregion
        }

        [TestMethod]
        public void TestDetails_InvalidAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Details(1000);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestDetails_UnauthorizedAccountOwner()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Details(1);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        [TestMethod]
        public void TestDetails_AuthorizedAccountOwner()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserA")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Details(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as Account).CustomerID, 0);

            #endregion
        }

        [TestMethod]
        public void TestCreateGet()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserA")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

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

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserA")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Create(tAccount);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as Account).AccountBalance, -100.0);

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

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserA")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Create(tAccount);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Details");
            Assert.AreEqual((tResult as RedirectToActionResult).RouteValues["id"], 3);

            #endregion
        }

        [TestMethod]
        public void TestDepositGet_Null()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserA")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

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

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "User")})
                });

            var serviceProviderMock = new Mock<IServiceProvider>();
            var tempDataFactoryMock = new Mock<ITempDataDictionaryFactory>();
            var UrlFactoryMock = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataFactoryMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(UrlFactoryMock.Object);


            var httpContext = Substitute.For<HttpContext>();
            httpContext.RequestServices = serviceProviderMock.Object;
            httpContext.User.Returns(validPrincipal);

            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            tController = new AccountsController(tRepo)
            {
                ControllerContext = contContext,
            };

            #endregion

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

      
    }
}
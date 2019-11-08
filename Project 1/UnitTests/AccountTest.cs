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

        #region DETAILS

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

        #endregion

        #region CREATE

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
            Assert.AreEqual((tResult as RedirectToActionResult).RouteValues["id"], 4);

            #endregion
        }

        #endregion

        #region DEPOSIT

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

        [TestMethod]
        public void TestDepositGet_NonDepositAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Deposit(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        #endregion

        #region WITHDRAW

        [TestMethod]
        public void TestWithdrawGet_Null()
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

            var tResult = tController.Withdraw(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestWithdrawGet_UnautorizedAccountOwner()
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

            var tResult = tController.Withdraw(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestWithdrawGet_NonWithdrawAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Withdraw(3);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestWithdrawGet_ValidAccount()
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

            var tResult = tController.Withdraw(0);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.ID, 0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Amount, 0.0);

            #endregion
        }

        [TestMethod]
        public void TestWithdrawPost_MismatchAccountID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

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

            var tResult = tController.Withdraw(100, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestWithdrawPost_UnauthorizeAccountUser()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

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

            var tResult = tController.Withdraw(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestWithdrawPost_NonWithdrawAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 3);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Withdraw(3, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestWithdrawPost_OverdraftError()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 5000.0 };

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

            var tResult = tController.Withdraw(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.ID, 0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.AccountBalance, 1000.0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Amount, 0.0);
            Assert.IsNotNull((tResult as ViewResult).ViewData["ErrorMessage"]);

            #endregion
        }

        [TestMethod]
        public void TestWithdrawPost_Valid()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(0, 0);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 1000.0 };

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

            var tResult = tController.Withdraw(0, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        #endregion

        #region INSTALLMENT

        [TestMethod]
        public void TestInstallmentGet_Null()
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

            var tResult = tController.Installment(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestInstallmentGet_UnautorizedAccountOwner()
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

            var tResult = tController.Installment(2);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            
            #endregion
        }

        [TestMethod]
        public void TestInstallmentGet_NonLoanAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Installment(2);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestInstallmentGet_ValidAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Installment(3);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.ID, 3);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.AccountTypeID, (int)Utility.AccountType.LOAN);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Amount, 0.0);

            #endregion
        }

        [TestMethod]
        public void TestInstallmentPost_MismatchAccountID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 3);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

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

            var tResult = tController.Installment(100, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestInstallmentPost_UnauthorizeAccountUser()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 3);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

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

            var tResult = tController.Installment(3, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestInstallmentPost_NonLoanAccount()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 3);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 0.0 };

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Installment(3, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestInstallmentPost_OverdraftError()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 3);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 100000.0 };

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Installment(3, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is ViewResult);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.ID, 3);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Account.AccountBalance, 50000.0);
            Assert.AreEqual(((tResult as ViewResult).Model as AccountTransactionVM).Amount, 0.0);
            Assert.IsNotNull((tResult as ViewResult).ViewData["ErrorMessage"]);

            #endregion
        }

        [TestMethod]
        public void TestInstallmentPost_Valid()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            AccountsController tController = null;
            Account tAccount = tRepo.GetAccountInformation(1, 3);
            AccountTransactionVM tVM = new AccountTransactionVM() { Account = tAccount, Amount = 5000.0 };

            #region DO NOT DELETE - FOR GENERATING FAKE SESSION USER DATA

            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserB")})
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

            var tResult = tController.Installment(3, tVM);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");
            Assert.AreEqual((tResult as RedirectToActionResult).ControllerName, "Customers");

            #endregion
        }

        #endregion

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
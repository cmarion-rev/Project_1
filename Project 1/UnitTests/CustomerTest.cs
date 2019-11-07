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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Create();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestDetails_ExistingCustomer()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Details(null) as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as Customer;
            Assert.AreEqual(tValue.FirstName, "John");

            #endregion
        }

        [TestMethod]
        public void TestDetails_NewCustomer()
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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Details(null);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");

            #endregion
        }

        [TestMethod]
        public void TestIndex_NewCustomer()
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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Index();

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");

            #endregion
        }

        [TestMethod]
        public void TestIndex_ExistingCustomer()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Index() as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as CustomerAccountsVM;
            Assert.AreEqual(tValue.Customer.FirstName, "John");
            Assert.AreEqual(tValue.Accounts.Count, 2);

            #endregion
        }

        [TestMethod]
        public void TestEditGet_ExistingCustomer()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Edit(10) as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as Customer;
            Assert.AreEqual(tValue.FirstName, "Mary");

            #endregion
        }

        [TestMethod]
        public void TestEditGet_ExistingCustomer_InvalidUser()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Edit(10) as ViewResult;

            #endregion

            #region ASSERT

            var tValue = tResult.Model as Customer;
            Assert.AreNotEqual(tValue.FirstName, "Mary");

            #endregion
        }

        [TestMethod]
        public void TestEditGet_NewCustomer()
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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Edit(1);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Create");

            #endregion
        }

        [TestMethod]
        public void TestEditPost_ExistingCustomer_ValidID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;
            Customer tCustomer = new Customer()
            {
                FirstName = "Mary",
                ID = 1,
                UserIdentity = "UserB"
            };

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Edit(1, tCustomer);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is RedirectToActionResult);
            Assert.AreEqual((tResult as RedirectToActionResult).ActionName, "Index");

            #endregion
        }

        [TestMethod]
        public void TestEditPost_ExistingCustomer_InvalidID()
        {
            #region ASSIGN

            TestRepository tRepo = new TestRepository();
            CustomersController tController = null;
            Customer tCustomer = new Customer()
            {
                FirstName = "Mary",
                ID = 1,
                UserIdentity = "UserB"
            };

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


            tController = new CustomersController(tRepo)
            {
                ControllerContext = contContext,
                //HttpContext = httpContext,
            };

            #endregion

            #endregion

            #region ACT

            var tResult = tController.Edit(0, tCustomer);

            #endregion

            #region ASSERT

            Assert.IsTrue(tResult is UnauthorizedResult);

            #endregion
        }
    }
}
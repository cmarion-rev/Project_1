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
            var urlHelperFactory = new Mock<ITempDataDictionaryFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(urlHelperFactory.Object);


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

            var tResult = tController.Create() as ViewResult;

            #endregion

            #region ASSERT

            var tList = tResult.ViewData["StateID"] as SelectList;

            var tItem = tList.Where(a => a.Value == "0").FirstOrDefault().Text;

            Assert.AreEqual(tItem, "FL");

            #endregion
        }
    }
}
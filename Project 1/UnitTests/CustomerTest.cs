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

namespace UnitTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestRepository testRepo = new TestRepository();
            CustomersController testController = null;

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
            

            testController = new CustomersController(testRepo)
            {
              ControllerContext = contContext,   
                 //HttpContext = httpContext,
            };

            #endregion

            var returnUser = testController.User;

            var testResult = testController.Create();


            var S = testResult;

        }
    }
}

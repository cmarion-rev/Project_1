using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Security.Claims;
using UnitTests.Objects;
using UnitTests.Repositories;
using Web_Interface.Controllers;

namespace UnitTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestRepository testRepo = new TestRepository();
            CustomersController testController = new CustomersController(testRepo);


            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, "UserA")})
                });

            var httpContext = Substitute.For<HttpContext>();
            httpContext.User.Returns(validPrincipal);
            var contContext = Substitute.For<ControllerContext>();
            contContext.HttpContext = httpContext;


            testController = new CustomersController(testRepo)
            {
              ControllerContext = contContext,   
                 //HttpContext = httpContext,
            };

            var returnUser = testController.User;



            testController.Create();

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NSubstitute;
using System;
using System.Security.Claims;

namespace UnitTests.Resources
{
    static class UtilityFunctions
    {
        public static ControllerContext GenerateMockControllerContext(string userID)
        {
            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                     new ClaimsIdentity(
                         new[] {new Claim(ClaimTypes.NameIdentifier, userID)})
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
            return contContext;
        }
    }
}

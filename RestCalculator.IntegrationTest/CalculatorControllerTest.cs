using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using RestCalculator.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestCalculator.IntegrationTest
{
    [TestFixture]
    public class CalculatorControllerTest
    {
        [Test]
        public async Task CheckStatus_SendRequest_ShouldRetyurnOk()
        {
            // Arrange
            var webHost = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(_ => { });
            var httpClent = webHost.CreateClient();

            // Act
            var response = await httpClent.GetAsync("/Calculator/Add?a=1&b=1");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DivideByZero_CalculatorDivide_ShouldReturnBadRequest()
        {
            // Arrange
            var webHost = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var calculatorService = services.SingleOrDefault(x => x.ServiceType is ICalculator);
                        services.Remove(calculatorService);

                        var mockService = new Mock<ICalculator>();
                        mockService
                            .Setup(_ => _.Divide(It.IsAny<double>(), It.IsAny<double>()))
                            .Throws<DivideByZeroException>();

                        services.AddTransient(_ => mockService.Object);
                    });
                });

            var httpClent = webHost.CreateClient();

            // Act
            var response = await httpClent.GetAsync("/Calculator/Div?a=1&b=1");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}

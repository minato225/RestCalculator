using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using RestCalculator.Controllers;
using RestCalculator.Model;
using RestCalculator.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
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
            var result = await httpClent.GetFromJsonAsync<CalculatorResult>("/Calculator/Div?a=1&b=1");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.ErrorMessage, Is.Not.Null);
            });
        }

        [Test]
        public async Task BehavourControllerTest()
        {
            // Arrange
            var mockCalculatorService = new Mock<ICalculator>();
            mockCalculatorService
                .Setup(_ => _.Add(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(It.IsAny<double>());

            var calculatorController = new CalculatorController(mockCalculatorService.Object);

            // Act

            var result = await calculatorController.Add(1, 1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(okResult, Is.Not.Null);
            });

            mockCalculatorService
                .Verify(_ => _.Add(It.IsAny<double>(), It.IsAny<double>()),
                () => Times.AtLeastOnce());
        }
    }
}

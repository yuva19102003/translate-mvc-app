using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using TranslatorMVC.Controllers;
using TranslatorMVC.Models;
using Xunit;

namespace TranslatorMVC.Tests
{
    public class HomeControllerTests
    {
        [Theory]
        [InlineData("Hello", "en", "fr", "Bonjour")]
        [InlineData("Hello", "en", "de", "Hallo")]
        [InlineData("Hello", "en", "es", "Hola")]
        [InlineData("Hello", "en", "hi", "नमस्ते")]
        [InlineData("Hello", "en", "ta", "வணக்கம்")]
        public async Task Post_Translate_Returns_Result(string text, string source, string target, string expected)
        {
            // 1️⃣ Arrange
            var fakeJson = $"{{\"translation\":\"{expected}\"}}";

            var handler = new FakeHttpMessageHandler(fakeJson);
            var client = new HttpClient(handler);

            // Mock IHttpClientFactory
            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Mock IConfiguration
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["LingvaApi:BaseUrl"]).Returns("https://fakeapi.com");

            // Create controller with both mocks
            var controller = new HomeController(factoryMock.Object, configMock.Object);

            // 2️⃣ Act
            var result = await controller.Index(new TranslationRequest
            {
                Text = text,
                SourceLang = source,
                TargetLang = target
            });

            // 3️⃣ Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TranslationRequest>(viewResult.Model);
            Assert.Equal(expected, model.TranslatedText);
        }
    }
}

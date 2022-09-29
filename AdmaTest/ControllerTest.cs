using DnssWebApi;
using DnssWebApi.Controllers;
using DnssWebApi.Dto;
using DnssWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AdmaTest
{
    public class ControllerTest
    {
        private  Mock<InMemModelRepo> repositoryTest = new();
        private  Mock<ILogger<AdmaController>> loggerMess = new();
        private readonly Random rnd = new Random();


        [Fact]
        public async Task GetModelAsync_WithUnExistingModel_ReturnsNotFound()
        {
            // Arrange
            repositoryTest = new Mock<InMemModelRepo>();
            //repositoryTest.Setup(repo => repo.GetModel(It.IsAny<int>())).ReturnsAsync((AdmaModel)null);

            loggerMess = new Mock<ILogger<AdmaController>>();

            var controller = new AdmaController(repositoryTest.Object, loggerMess.Object);

            // Act
            ActionResult<AdmaModel> result = await controller.GetModel(9999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            
        }

        [Fact]
        public async Task GetModelAsync_WithExpectingItem_ReturnsNotFound()
        {
            var expectedModel = CreateRandomModel();

            repositoryTest = new Mock<InMemModelRepo>();

            var controller = new AdmaController(repositoryTest.Object, loggerMess.Object);

            // Act
            ActionResult<AdmaModel> result = await controller.GetModel(expectedModel.ID);

            // Assert
            Assert.Equal(expectedModel.ID, result.Value.ID);
            Assert.Equal(expectedModel.ID, result.Value.ID);
        }

        private AdmaModel CreateRandomModel()
        {
            return new()
            {
                ID = rnd.Next(1, 100),
                Default = rnd.Next(1, 100),
                Inaktiv = rnd.Next(100) <= 20 ? true : false,
                Minimum = rnd.Next(1, 10),
                Maximum = rnd.Next(11, 100),
                Propertiers = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", rnd.Next(4, 12)).ToString()),
                Type = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", rnd.Next(1, 10)).ToString()),
                Resolution = rnd.Next(1, 10),
                Unit = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", rnd.Next(1, 4)).ToString()),
                Value = rnd.Next(1, 1000)
            };
        }
    }
}

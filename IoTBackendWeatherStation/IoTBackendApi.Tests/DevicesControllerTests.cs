using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IoTBackendApi.Controllers;
using IoTBackendApi.Models.Domain;
using IoTBackendApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace IoTBackendApi.Tests
{
    [TestFixture]
    public class DevicesControllerTests
    {
        private Mock<IIotDataService> _mockIotDataService;

        [SetUp]
        public void Setup()
        {
            _mockIotDataService = new Mock<IIotDataService>();
        }

        [Test]
        public async Task GetSensorDataForDate_Should_Return_Not_Found_For_Empty_Result()
        {
            // Arrange
            List<SensorResult> sensorResults = new List<SensorResult>();
            _mockIotDataService.Setup(i => i.GetSensorDataForDate(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(sensorResults);
            var controller = new DevicesController(_mockIotDataService.Object);

            // Act
            var result = await controller.GetSensorDataForDate("dockan", new DateTime());

            // Assert
            result.GetType().Name.Should().Be(typeof(NotFoundResult).Name);
        }

        [Test]
        public async Task GetSensorDataForDate_Should_Return_Ok_Object_Result()
        {
            // Arrange
            List<SensorResult> sensorResults = new List<SensorResult>()
            {
                new SensorResult(),
            };
            _mockIotDataService.Setup(i => i.GetSensorDataForDate(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(sensorResults);
            var controller = new DevicesController(_mockIotDataService.Object);

            // Act
            var result = await controller.GetSensorDataForDate("dockan", new DateTime());

            // Assert
            result.GetType().Name.Should().Be(typeof(OkObjectResult).Name);
            var okObjectResult = result as OkObjectResult;
            var resultObject = (IEnumerable<SensorResult>) okObjectResult.Value;
            resultObject.Count().Should().Be(1);
        }

        [Test]
        public async Task Get_Sensor_Data_For_Date_And_Sensor_Should_Return_Not_Found_For_Empty_Result()
        {
            // Arrange
            SensorResult nullResult = null;
            _mockIotDataService.Setup(
                i => i.GetSensorDataForDateAndSensor(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(nullResult);
            var controller = new DevicesController(_mockIotDataService.Object);

            // Act
            var result = await controller.GetSensorDataForDateAndSensor("dockan", new DateTime(), "temperature");

            // Assert
            result.GetType().Name.Should().Be(typeof(NotFoundResult).Name);
        }

        [Test]
        public async Task Get_Sensor_Data_For_Date_And_Sensor_Should_Return_Ok_Object_Result()
        {
            // Arrange
            SensorResult sensorResult = new SensorResult();
            sensorResult.SensorId = "temperature";
            _mockIotDataService.Setup(
                    i => i.GetSensorDataForDateAndSensor(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(sensorResult);
            var controller = new DevicesController(_mockIotDataService.Object);

            // Act
            var result = await controller.GetSensorDataForDateAndSensor("dockan", new DateTime(), "temperature");

            // Assert
            result.GetType().Name.Should().Be(typeof(OkObjectResult).Name);
            var okObjectResult = result as OkObjectResult;
            var resultObject = (SensorResult)okObjectResult.Value;
            resultObject.SensorId.Should().Be("temperature");
        }
    }
}

using System;
using System.Linq;
using GraduationGuideline.web.Controllers;
using Xunit;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.services;
using GraduationGuideline.domain.interfaces;
using Moq;
using System.Collections.Generic;
using GraduationGuideline.domain.DataTransferObjects;

namespace GraduationGuideline.test.domain.services
{
    public class StepServiceTests
    {


        [Fact]
        public void StepService_GetStepsByUser_ReturnsListOfSteps()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var steps = new List<StepDto>() {
                new StepDto { StepName = "one", Status = true, Username = "jmmalouf" },
                new StepDto { StepName = "two", Status = false, Username = "jmmalouf" }
            };

            mock.Setup(p => p.GetStepsByUsername("jmmaloof")).Returns(steps);
            var target = new StepService(mock.Object);


            // Act
            var data = target.GetStepsByUsername("jmmaloof");

            // Assert
            Assert.Equal(data, steps);
        }

        [Fact]
        public void WeatherService_WeatherForecasts_ForecastDataIsPopulated()
        {
            // Arrange
            var target = new WeatherService();

            // Act
            var result = target.GetForecast();

            // Assert
            Assert.NotNull(result.FirstOrDefault().DateFormatted);
            Assert.NotNull(result.FirstOrDefault().Summary);
            Assert.NotNull(result.FirstOrDefault().TemperatureC);
            Assert.NotNull(result.FirstOrDefault().TemperatureF);
        }
    }
}
using System;
using System.Linq;
using GraduationGuideline.web.Controllers;
using Xunit;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.services;

namespace GraduationGuideline.test.domain.services
{
    public class WeatherServiceTests
    {
        [Fact]
        public void WeatherService_WeatherForecasts_ShouldReturnFiveItems()
        {
            // Arrange
            var target = new WeatherService();
            
            // Act
            var result = target.GetForecast();

            // Assert
            Assert.Equal(5, result.Count());
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
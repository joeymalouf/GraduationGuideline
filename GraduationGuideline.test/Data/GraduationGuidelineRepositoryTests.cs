using System;
using System.Linq;
using GraduationGuideline.web.Controllers;
using Xunit;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.services;
using GraduationGuideline.data;
using GraduationGuideline.data.entities;
using System.Collections.Generic;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.domain.interfaces;

namespace GraduationGuideline.test.data
{
    public class RepositoryTests
    {
        private GraduationGuidelineContext _graduationGuidelineContext;
        private GraduationGuidelineRepository repo = new GraduationGuidelineRepository(new GraduationGuidelineContext());

        [Fact]
        public void CreateUserWorks()
        {
            //Given
            var user = new FullUserDto
            {
                Username = "jmmalouf",
                Password = "yes",
                StudentType = "PhD",
                Email = "jmmalouf@go.olemiss.edu",
                FirstName = "Joey",
                LastName = "Malouf",
                Admin = false
            };
            //When
            repo.CreateUser(user);
            var result = repo.GetUserByUsername("jmmalouf");
            //Then
            Assert.Equal(result.Username, user.Username);
        }

        [Fact]
        public void CreateUserThrowsExceptioWhenUserExists()
        {
            //Given
            var user = new FullUserDto
            {
                Username = "jmmalouf",
                Password = "yes",
                StudentType = "PhD",
                Email = "jmmalouf@go.olemiss.edu",
                FirstName = "Joey",
                LastName = "Malouf",
                Admin = false
            };
            //When
            //Then
            Assert.Throws<ArgumentException>(() => repo.CreateUser(user));
        }

        [Fact]
        public void EditUserWorks()
        {
            //Given
            var user = new FullUserDto
            {
                Username = "jmmalouf",
                Password = "yes",
                StudentType = "PhD",
                Email = "jmmalouf@go.olemiss.edu",
                FirstName = "Joey",
                LastName = "Malouf",
                Admin = false
            };
            repo.CreateUser(user);

            var userEdit = new UserInfoDto
            {
                Username = "jmmalouf",
                StudentType = "MasterT",
                Email = "jmmalouf@go.olemiss.edu",
                FirstName = "Joey",
                LastName = "Malouf",
                Admin = false
            };
            //When
            repo.EditUser(userEdit);
            var result = repo.GetUserByUsername("jmmalouf");

            //Then
            Assert.Equal(result.StudentType, "MasterT");

        }

        [Fact]
        public void DeleteUserWorks()
        {
            //Given
            var user = new FullUserDto
            {
                Username = "asdasdasd",
                Password = "yess",
                StudentType = "PhD",
                Email = "jmmalouf@go.olemiss.edu",
                FirstName = "Joey",
                LastName = "Malouf",
                Admin = false
            };
            //When
            repo.CreateUser(user);
            repo.RemoveUser(user.Username);
            var result = repo.GetUserByUsername("asdasdasd");
            //Then
            Assert.Equal(result, null);
        }

        [Fact]
        public void GetStepsByUser()
        {
            //Given
            var steps = new List<StepEntity>() {
            new StepEntity { StepName = "one", Status = true, Username = "jmmalouf" },
            new StepEntity { StepName = "two", Status = false, Username = "jmmalouf" }
        };
            var user = new UserEntity
            {
                Username = "jmmalouf",
                Password = "yes",
                StudentType = "PhD",
                Email = "jmmalouf@go.olemiss.edu",
                FirstName = "Joey",
                LastName = "Malouf",
                Admin = false
            };
            //When

            //Then
        }
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
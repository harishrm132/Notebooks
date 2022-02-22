using CloudCustomers.Api.Controllers;
using CloudCustomers.Api.Models;
using CloudCustomers.Api.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers
{
    
    public class TestUserController
    {
        [Fact]
        public async Task Get_OnSucess_ReturnsStatusCode200()
        {
            //Arrage - Setup system under test
            var mockUsersService = new Mock<IUserService>();
            //Setup mock and control the behaviour of the our dependencies 
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers());
            var sut = new UserController(mockUsersService.Object);

            //Act - Make something happen by calling methods
            var result = (OkObjectResult)await sut.Get();


            //Assert - Assertion of outcome of particular arragment and actions
            result.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task Get_OnSucess_InvokeUserServiceExcatlyOnce()
        {
            //Arrage
            var mockUsersService = new Mock<IUserService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UserController(mockUsersService.Object);

            //Act 
            var result = await sut.Get();

            //Assert
            mockUsersService.Verify(service => service.GetAllUsers(), Times.Once());
        }

        [Fact]
        public async Task Get_OnSucess_ReturnsListofUsers()
        {
            //Arrage
            var mockUsersService = new Mock<IUserService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers());
            var sut = new UserController(mockUsersService.Object);

            //Act 
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task Get_OnNoUsersFound_Returns404()
        {
            //Arrage 
            var mockUsersService = new Mock<IUserService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UserController(mockUsersService.Object);

            //Act 
            var result = await sut.Get();


            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        
    }
}

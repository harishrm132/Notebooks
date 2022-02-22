using CloudCustomers.Api.Config;
using CloudCustomers.Api.Models;
using CloudCustomers.Api.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicsGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/Users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UserService(httpClient, config);
            
            //Act 
            await sut.GetAllUsers();

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );

            //Verify HTTP request is made!
            
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListofUsers()
        {
            //Arrage
            var handlerMock = MockHttpMessageHandler<User>.SetupReturns404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/Users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UserService(httpClient, config);

            //Act 
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListofUsersofExpectedSize()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicsGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/Users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UserService(httpClient, config);

            //Act 
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }
        
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrage
            var expectedResponse = UserFixture.GetTestUsers();
            //var endPoint = "https://example.com/users";
            var endPoint = "https://jsonplaceholder.typicode.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicsGetResourceList(expectedResponse, endPoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UserService(httpClient, config);

            //Act 
            var result = await sut.GetAllUsers();
            var uri = new Uri(endPoint);

            //Assert
            handlerMock
               .Protected()
               .Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
               ItExpr.IsAny<CancellationToken>()
               );
        }
    }
}

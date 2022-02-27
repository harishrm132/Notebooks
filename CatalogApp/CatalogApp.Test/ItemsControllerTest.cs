using CatalogApp.Api.Controllers;
using CatalogApp.Api.DTOs;
using CatalogApp.Api.Entities;
using CatalogApp.Api.Repositories;
using CatalogApp.Test.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CatalogApp.Test
{
    public class ItemsControllerTest
    {
        private Mock<IInMenuItemRepository> repositoryStub = new();
        private Mock<ILogger<ItemsController>> loggerStub = new();

        //UnitofWork_StateUnderTest_ExpectedBehaviour
        [Fact]
        public async Task GetItemAsync_WithNoItem_ReturnsNotFound()
        {
            //Arrage
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Item)null);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
        {
            //Arrage
            var expectedItem = CatalogItemFixture.GetTestItem();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedItem);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            //Assert
            result.Value.Should().BeEquivalentTo(expectedItem,
                options => options.ComparingByMembers<Item>());
        }

        [Fact]
        public async Task GetItemsAsync_WithExistingItems_ReturnsAllItems()
        {
            //Arrage
            var expectedItems = new[] { CatalogItemFixture.GetTestItem(), CatalogItemFixture.GetTestItem() };
            repositoryStub.Setup(repo => repo.GetItemsAsync())
                .ReturnsAsync(expectedItems);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemsAsync();

            //Assert
            result.Should().BeEquivalentTo(expectedItems,
                options => options.ComparingByMembers<Item>());
        }
        
        [Fact]
        public async Task CreateItemAsync_WithItemtoCreate_ReturnsCreatedItem()
        {
            //Arrage
            var itemtoCreate = new CreateItemDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = 40
            };
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.CreateItemAsync(itemtoCreate);
            var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;

            //Assert
            itemtoCreate.Should().BeEquivalentTo(createdItem,
                options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers());
            createdItem.Id.Should().NotBeEmpty();
            createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, new TimeSpan(24, 0, 0));
        }
        
        [Fact]
        public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
        {
            //Arrage
            var existingItem = CatalogItemFixture.GetTestItem();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingItem);
            var itemId = existingItem.Id;
            var itemtoUpdate = new UpdateItemDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = existingItem.Price + 3
            };

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.UpdateItemAsync(itemId, itemtoUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public async Task DeleteItemAsync_WithExistingItem_ReturnsNoContent()
        {
            //Arrage
            var existingItem = CatalogItemFixture.GetTestItem();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingItem);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.DeleteItemAsync(existingItem.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

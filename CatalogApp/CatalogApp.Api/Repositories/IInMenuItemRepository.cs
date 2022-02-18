using CatalogApp.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogApp.Api.Repositories
{
    public interface IInMenuItemRepository
    {
        Task<Item> GetItemAync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}
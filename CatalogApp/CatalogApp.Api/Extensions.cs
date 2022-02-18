using CatalogApp.Api.DTOs;
using CatalogApp.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApp.Api
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item i)
        {
            return new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                CreatedDate = i.CreatedDate
            };
        }
    }
}

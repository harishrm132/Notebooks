using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApp.Api.DTOs
{
    //Implement Data Trasfer Objects (DTOs) - Actual Contract between Client and Service
    public record ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}

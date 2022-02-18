using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApp.Api.DTOs
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
    }
}

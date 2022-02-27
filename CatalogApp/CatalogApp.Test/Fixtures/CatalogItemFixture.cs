using CatalogApp.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogApp.Test.Fixtures
{
    internal class CatalogItemFixture
    {
        internal static Item GetTestItem()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = 40,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}

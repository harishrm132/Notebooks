using System;

namespace CatalogApp.Api.Entities{

    //Record Types - better support for Immutable objects, With expression support
    public record Item 
    {
        public Guid Id { get; init; } //init - initializes the prop only in constructor
        public string Name { get; set; }       
        public decimal Price { get; set; } 
        public DateTimeOffset CreatedDate { get; init; }

        //Repository - Class inchange of storing item in DB(system) 
    }
}
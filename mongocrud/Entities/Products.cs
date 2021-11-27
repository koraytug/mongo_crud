using System;
using System.Collections.Generic;

namespace mongocrud.Entities
{
    public record Products()
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; } 
        public decimal Price { get; init; }
        public string Currency { get; init; }
    }

    public record ProductLookedUp : Products
    {
        public List<Categories> CategoryList { get; set; }
    }
}

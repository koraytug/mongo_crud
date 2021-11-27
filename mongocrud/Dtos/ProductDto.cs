using System;
using mongocrud.Entities;

namespace mongocrud.Dtos
{
    public record ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }
    }
}

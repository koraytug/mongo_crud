using System;
using mongocrud.Entities;

namespace mongocrud.Dtos
{
    public record ProductResultDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public Categories Category { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }
    }
}

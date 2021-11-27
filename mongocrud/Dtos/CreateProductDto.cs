using System;
using System.ComponentModel.DataAnnotations;

namespace mongocrud.Dtos
{
    public record CreateProductDto
    { 
        public Guid Id { get; init; }

        [Required]
        public string Name { get; init; }

        public string Description { get; init; }

        public string CategoryId { get; init; }

        [Required]
        public decimal Price { get; init; }

        [Required]
        public string Currency { get; init; }
    }
}

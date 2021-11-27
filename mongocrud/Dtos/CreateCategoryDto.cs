using System;
using System.ComponentModel.DataAnnotations;

namespace mongocrud.Dtos
{
    public record CreateCategoryDto
    { 
        public Guid Id { get; init; }

        [Required]
        public string Name { get; init; }

        public string Description { get; init; }
    }
}

﻿using System;

namespace mongocrud.Dtos
{
    public record UpdateCategoryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}

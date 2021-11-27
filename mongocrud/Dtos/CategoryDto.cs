﻿using System;
namespace mongocrud.Dtos
{
    public record CategoryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}

using System;
using System.Collections.Generic;

namespace mongocrud.Entities
{
    public record Categories
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }  
}

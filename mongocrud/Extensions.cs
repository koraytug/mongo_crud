using System;
using System.Collections.Generic;
using mongocrud.Dtos;
using mongocrud.Entities;

namespace mongocrud
{
    public static class Extensions
    {
        public static CategoryDto AsCategoryDto(this Categories category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static ProductDto AsProductDto(this Products product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Currency = product.Currency
            };
        }

        public static ProductResultDto AsProductResultDto(this ProductLookedUp product)
        {
          
            return new ProductResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.CategoryList.Count>0? product.CategoryList[0]: new Categories(),
                Price = product.Price,
                Currency = product.Currency
            };
        }
    }
}

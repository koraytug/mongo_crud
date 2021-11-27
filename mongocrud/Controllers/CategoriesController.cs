using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mongocrud.Dtos;
using mongocrud.Entities;
using mongocrud.Repositories;

namespace mongocrud.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        // GET /categories
        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = repository.GetCategories().Select(category => category.AsCategoryDto());
            return categories;
        }

        // GET /categories/{id}
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetCategory(Guid id)
        {
            var item = repository.GetCategory(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsCategoryDto();
        }

        // POST /categories
        [HttpPost]
        public ActionResult<CategoryDto> CreateCategory(CreateCategoryDto categoryDto)
        {
            Categories category = new()
            {
                Id = Guid.NewGuid(),
                Name = categoryDto.Name,
                Description = categoryDto.Description 
            };

            repository.CreateCategory(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category.AsCategoryDto());
        }

        // PUT /categories/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateCategoryDto categoryDto)
        {
            var existingCategory = repository.GetCategory(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            Categories updatedCategory = existingCategory with
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            repository.UpdateCategory(updatedCategory);

            return NoContent();
        }

        // DELETE /categories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingCategory = repository.GetCategory(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            repository.DeleteCategory(id);

            return NoContent();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mongocrud.Dtos;
using mongocrud.Entities;
using mongocrud.Repositories;

namespace mongocrud.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // GET /products
        [HttpGet]
        public IEnumerable<ProductResultDto> GetProducts()
        {
            var products = repository.GetProducts().Select(product => product.AsProductResultDto());
            return products;
        }

        // GET /products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(Guid id)
        {

            ProductLookedUp product = await repository.GetProduct(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product.AsProductResultDto());
        }

        // POST /products
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto productDto)
        {
            Products product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                Currency = productDto.Currency
            };

            repository.CreateProduct(product);

            return Ok(CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.AsProductDto()));
        }

        // PUT /products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, UpdateProductDto productDto)
        {
            var existingProduct =  repository.GetProductUnRelated(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            Products updatedProduct = existingProduct with
            {
                Name = productDto.Name,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                Currency = productDto.Currency
            };

            repository.UpdateProduct(updatedProduct);

            return Ok();
        }

        // DELETE /products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var existingProduct = repository.GetProduct(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            repository.DeleteProduct(id);

            return NoContent();
        }

    }
}

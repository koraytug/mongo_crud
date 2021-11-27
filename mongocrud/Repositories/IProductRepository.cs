using System;
using mongocrud.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mongocrud.Repositories
{

    public interface IProductRepository
    {
        Task<ProductLookedUp> GetProduct(Guid id);
        Products GetProductUnRelated(Guid id);
        IEnumerable<ProductLookedUp> GetProducts();
        void CreateProduct(Products pruduct);
        void UpdateProduct(Products pruduct);
        void DeleteProduct(Guid id);
    }

}

using System;
using mongocrud.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace mongocrud.Repositories
{
    public class MongoDbProductRepository : IProductRepository
    {
        private const string databaseName = "catalog";
        private const string productsCollectionName = "products";
        private const string categoriesCollectionName = "categories";
        private readonly IMongoCollection<Products> productsCollection;
        private readonly IMongoCollection<Categories> categoriesCollection;
        private readonly FilterDefinitionBuilder<Products> filterBuilder = Builders<Products>.Filter;
        private readonly IDistributedCache _distributedCache;


        public MongoDbProductRepository(IMongoClient mongoClient, IDistributedCache distributedCache)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productsCollection = database.GetCollection<Products>(productsCollectionName);
            categoriesCollection = database.GetCollection<Categories>(categoriesCollectionName);
            _distributedCache = distributedCache;
        }

        public void CreateProduct(Products product)
        {
            productsCollection.InsertOne(product);
        }

        public void DeleteProduct(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            productsCollection.DeleteOne(filter);
        }

        public Products GetProductUnRelated(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return productsCollection.Find(filter).SingleOrDefault();
        }

        public async Task<ProductLookedUp> GetProduct(Guid id)
        {
            ProductLookedUp productResults;
            string serializedProducts;

            var encodedProducts = await _distributedCache.GetAsync(id.ToString());

            if (encodedProducts != null)
            {
                serializedProducts = Encoding.UTF8.GetString(encodedProducts);
                productResults = JsonConvert.DeserializeObject<ProductLookedUp>(serializedProducts);
            }
            else
            {
                var filter = filterBuilder.Eq(item => item.Id, id);

                productResults = productsCollection.Aggregate().Match(filter)
            .Lookup<Products, Categories, ProductLookedUp>(categoriesCollection, a => a.CategoryId, a => a.Id, a => a.CategoryList)
            .FirstOrDefault();

            serializedProducts = JsonConvert.SerializeObject(productResults);
                encodedProducts = Encoding.UTF8.GetBytes(serializedProducts);

                var option = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(5));

                await _distributedCache.SetAsync(id.ToString(), encodedProducts, option);

            }


            return productResults;
        }

        public IEnumerable<ProductLookedUp> GetProducts()
        {
            IEnumerable<ProductLookedUp> productResults;

            productResults = productsCollection.Aggregate()
             .Lookup<Products, Categories, ProductLookedUp>(categoriesCollection, a => a.CategoryId, a => a.Id, a => a.CategoryList).ToEnumerable();

            return productResults;
        }

        public void UpdateProduct(Products product)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, product.Id);
            productsCollection.ReplaceOne(filter, product);
        }
    }
}

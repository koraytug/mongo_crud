using System;
using mongocrud.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace mongocrud.Repositories
{
    public class MongoDbCategoryRepository : ICategoryRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "categories";
        private readonly IMongoCollection<Categories> itemsCollection;
        private readonly FilterDefinitionBuilder<Categories> filterBuilder = Builders<Categories>.Filter;

       
        public MongoDbCategoryRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Categories>(collectionName);
        }

        public void CreateCategory(Categories category)
        {
            itemsCollection.InsertOne(category);
        }

        public void DeleteCategory(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        public Categories GetCategory(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Categories> GetCategories()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateCategory(Categories category)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, category.Id);
            itemsCollection.ReplaceOne(filter, category);
        }
    }
}

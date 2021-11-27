using System;
using mongocrud.Entities;
using System.Collections.Generic;

namespace mongocrud.Repositories
{

    public interface ICategoryRepository
    {
        Categories GetCategory(Guid id);
        IEnumerable<Categories> GetCategories();
        void CreateCategory(Categories categories);
        void UpdateCategory(Categories categories);
        void DeleteCategory(Guid id);
    }

}

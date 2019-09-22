using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> categories;

        public ProductCategoryRepository()
        {
            categories = cache["productCategories"] as List<ProductCategory>;

            if(categories == null)
            {
                categories = new List<ProductCategory>();
            }
        }

        public List<ProductCategory> Collection()
        {
            return categories;
        }

        public void Commit()
        {
            cache["productCategories"] = categories;
        }

        public void Insert(ProductCategory productCategory)
        {
            categories.Add(productCategory);
        }

        public void Update(ProductCategory productCategory , string Id)
        {
            ProductCategory categoryToUpdate = categories.Find(c => c.Id == Id);

            if(categoryToUpdate == null)
            {
                throw new Exception("Not Found");
            }

            else
            {
                categoryToUpdate.Name = productCategory.Name;
            }
        }

        public void Delete(ProductCategory productCategory)
        {
            ProductCategory productCategoryToDelete = categories.Find(c => c.Id == productCategory.Id);

            if (productCategoryToDelete == null)
            {
                throw new Exception("Not Found");
            }

            else
            {
                categories.Remove(productCategory);
            }
        }

        public ProductCategory Find(string Id)
        {
            return categories.Find(c => c.Id ==Id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }

        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            // Find product to be updated
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            // If found then update it
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            // Else display not found error
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public Product Find(string Id)
        {
            // Find product to be updated
            Product product = products.Find(p => p.Id == Id);
            // If found then update it
            if (product != null)
            {
                return product;
            }
            // Else display not found error
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            // Returns internal list of Products
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            // Find product to be deleted
            Product productToDelete = products.Find(p => p.Id == Id);
            // If found then Delete it
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            // Else display not found error
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}

using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class ProductRepository : IRepository<Product>
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {
            products.Add(new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 });
            products.Add(new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M });
            products.Add(new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M });
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product Get(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            item.Id = _nextId++;
            products.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var oldItem = products.Find(p => p.Id == item.Id);
            if(oldItem == null || oldItem.Id == -1)
            {
                return false;
            }
            products.Remove(oldItem);
            products.Add(item);

            return true;
        }
    }
}
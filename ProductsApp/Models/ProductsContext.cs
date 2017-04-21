using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace ProductsApp.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=ProductsContext")
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
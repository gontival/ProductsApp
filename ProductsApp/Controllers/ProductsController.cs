using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Calling new ProductRepository() in the controller is not the best design, because it ties the controller to a particular implementation of IRepository.
        /// For a better approach, see Using the Web API Dependency Resolver.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection
        /// </summary>
        private IRepository<Product> _repository = new ProductRepository();

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = _repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));

            return _repository.GetAll().Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }
    }
}

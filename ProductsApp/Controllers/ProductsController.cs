using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Calling new ProductRepository() in the controller is not the best design, because it ties the controller to a particular implementation of IRepository.
        /// For a better approach, see Using the Web API Dependency Resolver.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection
        /// </summary>
        private IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
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

        /// <summary>
        /// The method takes a parameter of type Product. In Web API, parameters with complex types are deserialized from the request body. Therefore, we expect the client to send a serialized representation of a product object, in either XML or JSON format.
        /// Model Validation : https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage PostProduct(Product item)
        {
            if (ModelState.IsValid)
            {
                item = _repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.Id });
                response.Headers.Location = new Uri(uri);

                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public void PutProduct(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = id;
                if (!_repository.Update(product))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        public void DeleteProduct(int id)
        {
            Product item = _repository.Get(id);
            if (item == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            _repository.Remove(id);
        }
    }
}

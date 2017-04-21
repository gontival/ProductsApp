using Microsoft.Practices.Unity;
using ProductsApp.Models;
using ProductsApp.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProductsApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Restrict access for every Web Api Controller
            config.Filters.Add(new AuthorizeAttribute());

            // Register the IRepository intergace with Unity and then creates a UnityResolver
            var container = new UnityContainer();
            container.RegisterType<IRepository<Product>, ProductRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BoardGameLibrary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ///This handles the error to show if the values passed in are not valid.
            config.Filters.Add(new OtherClasses.ValidateModelAttribute());
        }
    }
}

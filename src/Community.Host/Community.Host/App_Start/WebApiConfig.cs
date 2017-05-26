using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using Community.Host.CustomRoute;

namespace Community.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
           // var cors = new EnableCorsAttribute("*", "*", "*");
          //  config.EnableCors(cors);
            // Web API routes

            var constraintsResolver = new DefaultInlineConstraintResolver();

            constraintsResolver.ConstraintMap.Add("enum", typeof(EnumConstraint));

            config.MapHttpAttributeRoutes(constraintsResolver); 
           // config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "customers",
                defaults: new { controller = "Customers", action = "Get" }
            );
           
        }
    }
}

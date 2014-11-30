using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiGeoMoniker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Route",
                routeTemplate: "api/{controller}/Company/{IdCompany}",
                defaults: new { IdCompany = RouteParameter.Optional }
            );
        }
    }
}

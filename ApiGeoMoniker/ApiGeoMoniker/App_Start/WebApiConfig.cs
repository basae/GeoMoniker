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
                name: "Point",
                routeTemplate: "api/route/{IdRoute}/point/{id}",
                defaults: new { IdRoute = RouteParameter.Optional, controller = "Point", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Oneness",
                routeTemplate: "api/company/{idCompany}/User/{idUser}/oneness/{idOneness}",
                defaults: new { idCompany = RouteParameter.Optional, controller = "Oneness", idUser = RouteParameter.Optional, idOneness= RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Turn",
                routeTemplate: "api/route/{IdRoute}/turn/{id}",
                defaults: new { IdRoute = RouteParameter.Optional, controller = "Turn", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TurnControl",
                routeTemplate: "api/turncontrol/oneness/{OnenessId}/",
                defaults: new { OnenessId = RouteParameter.Optional, controller = "TurnControl" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}

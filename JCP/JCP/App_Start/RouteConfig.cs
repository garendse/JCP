using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JCP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{fileName}.png");
            routes.IgnoreRoute("{fileName}.svg");
            routes.IgnoreRoute("{fileName}.js");
            routes.IgnoreRoute("{fileName}.css");

            routes.MapRoute(
                name: "Default",
                url: "{*path}",
                defaults: new { controller = "Home", action = "Index"}
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAspFindGuide.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
         //   routes.MapRoute(
         //    name: "Personal",
         //    url: "Personal-Page-{action}-{id}",
         //    defaults: new { controller = "Personal_Page", action = "Index", id = UrlParameter.Optional }
         //);
         //   routes.MapRoute(
         //     name: "Guide",
         //     url: "Guide-Page-{action}-{id}",
         //     defaults: new { controller = "Guide_Page", action = "Index", id = UrlParameter.Optional }
         // );
            routes.MapRoute( 
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
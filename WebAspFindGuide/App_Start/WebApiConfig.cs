using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace WebAspFindGuide.App_Start
{
    public class WebApiConfig
    {
            public static void Register(HttpConfiguration config)
            {
                config.MapHttpAttributeRoutes();
                config.Formatters.Clear();
                config.Formatters.Add(new JsonMediaTypeFormatter());
          
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            }
        
    }
}
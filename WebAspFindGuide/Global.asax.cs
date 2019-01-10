using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAspFindGuide.App_Start;
using WebAspFindGuide.Models.SystemModel;

namespace WebAspFindGuide
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //register for web API
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            // get current context
            HttpContext currentContext = HttpContext.Current;

            if (currentContext != null)
            {
                if (!currentContext.Request.Browser.Crawler)
                {
                    WebsiteVisitor currentVisitor = new WebsiteVisitor(currentContext);
                    OnlineVisitorsContainer.Visitors[currentVisitor.SessionId] = currentVisitor;
                }
            }
        }

        protected void Session_End(Object sender, EventArgs e)
        {


            if (this.Session != null)
            {
                WebsiteVisitor visitor;
                OnlineVisitorsContainer.Visitors.TryRemove(this.Session.SessionID, out visitor);
            }
        }

        //protected void Application_PreRequestHandlerExecute(object sender, EventArgs eventArgs)
        //{
        //    var session = HttpContext.Current.Session;
        //    if (session != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        if (OnlineVisitorsContainer.Visitors.ContainsKey(session.SessionID))
        //            OnlineVisitorsContainer.Visitors[session.SessionID].AuthUser = HttpContext.Current.User.Identity.Name;
        //    }
        //}
    }
}

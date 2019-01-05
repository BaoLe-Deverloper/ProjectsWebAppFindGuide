using System.Web.Mvc;

namespace WebAspFindGuide.Areas.Admin_Page
{
    public class Admin_PageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin_Page";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Page_default",
                "Admin_Page/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
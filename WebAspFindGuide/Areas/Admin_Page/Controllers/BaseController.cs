using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAspFindGuide.Areas.Admin_Page.Models.Objects;

namespace WebAspFindGuide.Areas.Admin_Page.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //kiểm tra cookie nếu có gán vào sesion
            if (Session[Common.Const.Session_Admin] == null)
            {
                HttpCookie use_cookie = Request.Cookies[Common.Const.Cookie_Admin];
                if (use_cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(use_cookie.Value);
                    Session[Common.Const.Session_Admin] = JsonConvert.DeserializeObject<Admin_Account>(ticket.UserData);
                }
            }

            // kiểm tra  tồn tại của sesion nếu không chuyển về trang login
            var session = Session[Common.Const.Session_Admin];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Account", Action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
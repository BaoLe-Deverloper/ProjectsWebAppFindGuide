using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers
{
   
    public class HomeController : Controller
    {
        Account_Model account_Model;
        public HomeController()
        {
            account_Model = new Account_Model();
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            if ( Session[Common.Const.Session_Account] ==null)
            {
                HttpCookie use_cookie = Request.Cookies[Common.Const.Cookie_User];
                if (use_cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(use_cookie.Value);
                    Session[Common.Const.Session_Account] = JsonConvert.DeserializeObject<CustomAccount>(ticket.UserData);
                }              
            }
            return View();
        }
    }
}
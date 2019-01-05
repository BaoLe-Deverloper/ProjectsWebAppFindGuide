using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebAspFindGuide.CustomAuthencation;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers
{
    [RoutePrefix("Personal")]
    [CustomAttribute(PermissionName = "Client|Admin|Guide")]
    public class Personal_PageController : Controller
    {
        Account_Model account_Model = new Account_Model();
        public Personal_PageController()
        {
            account_Model = new Account_Model();
        }
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {  
            if (Session[Common.Const.Session_Account] == null)
            {
                HttpCookie use_cookie = Request.Cookies[Common.Const.Cookie_User];
                if (use_cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(use_cookie.Value);
                    Session[Common.Const.Session_Account] = JsonConvert.DeserializeObject<CustomAccount>(ticket.UserData);
                }
            }
            CustomAccount acc = (CustomAccount)Session[Common.Const.Session_Account];
            Account account = account_Model.GetAccountByID(acc.AccountID);
            return View(account);
        }
        [Route("Messager")]
        [HttpGet]
        public ActionResult Messager ()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebAspFindGuide.CustomAuthencation;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers
{
    [CustomAttribute(PermissionName = "Admin,Guide")]
    public class Personal_PageController : Controller
    {
        Account_Model account_Model = new Account_Model();
        public Personal_PageController()
        {
            account_Model = new Account_Model();
        }
      
        [HttpGet]
        public ActionResult Index(string id)
        {

            Account account = account_Model.GetAccountByID(id);
            return View(account);
        }
    }
}
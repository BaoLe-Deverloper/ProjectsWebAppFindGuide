using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAspFindGuide.Areas.Admin_Page.Controllers
{
    public class UserManagementController : BaseController
    {
        // GET: Admin_Page/UserManagement
        [HttpGet]
        public ActionResult AllUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LockedUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Guide()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Client()
        {
            return View();
        }
    }
}
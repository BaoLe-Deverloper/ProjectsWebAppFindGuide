using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAspFindGuide.Areas.Admin_Page.Controllers
{
    public class HomeAdminController : BaseController
    {
        // GET: Admin_Page/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
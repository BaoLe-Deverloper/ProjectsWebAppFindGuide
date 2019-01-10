using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAspFindGuide.Areas.Admin_Page.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin_Page/Home
        public ActionResult Index()
        {
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            
                return View();
        }
    }
}
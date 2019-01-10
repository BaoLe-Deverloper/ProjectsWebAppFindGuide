using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAspFindGuide.Controllers
{
    public class Page_ErrorsController : Controller
    {
        // GET: Page_Errors
        public ActionResult Page_Error_404()
        {
            return View();
        }
        public ActionResult Page_Error_NotRole()
        {
            return View();
        }
        public ActionResult Page_Error_500()
        {
            return View();
        }
    }
}
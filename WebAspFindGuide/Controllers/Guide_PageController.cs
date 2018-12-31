using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAspFindGuide.CustomAuthencation;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers
{
  
    public class Guide_PageController : Controller
    {
        // GET: Page_Guide
        Guide_Model guide_Model;
        public Guide_PageController()
        {
            guide_Model = Guide_Model.Instance;
        }
        
        [HttpGet]
        public ActionResult Index(string id)
        {

            Account Guide = guide_Model.GetGuideByID(id);
            return View(Guide);
        }
    }
}
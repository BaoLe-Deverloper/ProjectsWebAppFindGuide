using System.Web.Mvc;
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
        [Route("Guide-Page-{id}")]
        [HttpGet]
        public ActionResult Index(string id)
        {

            CustomAccount Guide = guide_Model.GetGuideByID(id);
            return View(Guide);
        }
    }
}
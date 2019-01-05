using System.Web;
using System.Web.Optimization;

namespace WebAspFindGuide
{
    public class BundleConfig
    {
 
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/plugin").Include(
                      "~/Assets/js/jquery.min.js" ,             
                      "~/Assets/js/plugins/moment.min.js" ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Assets/js/scripts.min.js" ,
                     "~/Assets/js/popper.min.js" ,
                     "~/Assets/js/main.js" ,
                     "~/Assets/js/custom.js" ));
            bundles.Add(new ScriptBundle("~/admin/jquery").Include(
                 "~/Assets/Admin/js/jquery-2.1.1.js",
                 "~/Assets/Admin/js/bootstrap.min.js" ,
                "~/Assets/Admin/js/plugins/metisMenu/jquery.metisMenu.js",
                "~/Assets/Admin/js/plugins/slimscroll/jquery.slimscroll.js"
                ));
            bundles.Add(new ScriptBundle("~/admin/js").Include(
                 "~/Assets/Admin/js/inspinia.js",
                 "~/Assets/Admin/js/plugins/pace/pace.min.js"

                ));

             bundles.Add(new StyleBundle("~/app/css").Include(
                  "~/Assets/css/styles-merged.css",
                  "~/Assets/css/style.css" ,
                  "~/Assets/css/vendor/bootstrap.min.css",
                  "~/Assets/css/font-awesome/css/font-awesome.css",
                  "~/Assets/css/vendor/animate.css",
                  "~/Assets/css/custom.css" ));

            bundles.Add(new StyleBundle("~/admin/css").Include(
                "~/Assets/Admin/css/bootstrap.css",
                 "~/Assets/Admin/font-awesome/css/font-awesome.css"
               ));
            bundles.Add(new StyleBundle("~/admin/customcss").Include(
                 "~/Assets/Admin/css/style.css"
              ));

        }
    }
}

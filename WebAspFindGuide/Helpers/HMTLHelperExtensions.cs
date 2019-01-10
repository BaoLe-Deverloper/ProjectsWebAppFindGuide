using System;
using System.Web;
using System.Web.Mvc;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Helpers
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }
        public static bool CheckRoleUser(this HtmlHelper html, string PermissionName = null)
        {

            var Acc = (CustomAccount)HttpContext.Current.Session[Common.Const.Session_Account];

            if (Acc != null)
            {

                try
                {
                    var Roles = PermissionName.Split('|');
                    string role = Account_Model.Instance.GetRoleAccountByRoleID(Acc.Account_RoleID);
                    foreach (string Role in Roles)
                    {
                        if (!String.IsNullOrEmpty(role) && role == Role.Trim())
                            return true;
                    }

                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;

        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}
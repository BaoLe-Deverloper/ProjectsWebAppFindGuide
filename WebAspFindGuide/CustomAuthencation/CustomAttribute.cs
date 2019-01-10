using System;
using System.Web;
using System.Web.Mvc;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.CustomAuthencation
{
    public class CustomAttribute: AuthorizeAttribute
    {
        public string PermissionName;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var Acc = (CustomAccount)HttpContext.Current.Session[Common.Const.Session_Account];
          
            if(Acc !=null)
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
                catch(Exception)
                {
                    return false;
                }
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var router =  HttpContext.Current.Request.Url.AbsolutePath;
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Account", Action = "LogIn", ReturnUrl = router,Message= "You do not have sufficient permissions to access this page !" }));
            
        }
    }
}
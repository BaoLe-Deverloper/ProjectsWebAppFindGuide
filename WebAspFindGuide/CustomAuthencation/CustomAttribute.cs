using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.CustomAuthencation
{
    public class CustomAttribute: AuthorizeAttribute
    {
        public string PermissionName;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var Acc = (string[])HttpContext.Current.Session[Common.Const.Session_Account];
          
            if(Acc !=null)
            {
               
                try
                {
                    var Roles = PermissionName.Split(',');
                    string role = Account_Model.Instance.GetRoleAccountByRoleID(int.Parse(Acc[3]));
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
            
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Page_Errors", Action = "Page_Error_NotRole" }));
            
        }
    }
}
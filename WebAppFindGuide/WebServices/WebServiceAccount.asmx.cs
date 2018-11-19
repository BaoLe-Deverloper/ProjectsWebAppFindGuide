
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using WebAppFindGuide.Models;

namespace WebAppFindGuide
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        public SecurityAuthencationApp.Authenticity authenticity;
        DataAppDataContext DataAppData;
        public WebService()
        {
            DataAppData = new DataAppDataContext();
        }
        [WebMethod]
        public string AuthenticationMethod()
        {
            if (String.IsNullOrEmpty(authenticity.UserEmail))
                return "Please provide Email.";
            if (String.IsNullOrEmpty(authenticity.PassWord))
                return "Please provide PassWord.";

            var result = authenticity.IsValid(authenticity.UserEmail, authenticity.PassWord);
            if (result == false)
                return "Invalid Email or Password.";
            string Token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(Token, authenticity.UserEmail, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10), CacheItemPriority.NotRemovable, null);
            return Token;
        }
      
        [SoapHeader("authenticity", Required = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod (Description = "Login App")]
        public string GetListAccount(int ID)
        {
            if(authenticity != null)
            {
              if(authenticity.IsValid(authenticity))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var list = DataAppData.Accounts.Where(q => q.AccountID == ID).ToList();
                    return js.Serialize(list);
                }
                   
             
            }
            return "Please call AuthenticationMethod() !";



        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
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

        DataAppDataContext DataAppData;
        public WebService()
        {
            DataAppData = new DataAppDataContext();
        }
        [WebMethod]
       
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListAccount(int ID)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = DataAppData.Accounts.Where(q=>q.AccountID == ID ).ToList();
            return js.Serialize(list);

          
        }
    }
}

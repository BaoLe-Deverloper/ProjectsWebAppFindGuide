using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using WebAppFindGuide.Common;
using WebAppFindGuide.Models;

namespace WebAppFindGuide.SecurityAuthencationApp
{
    public class Authenticity : SoapHeader
    {
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
        public string Token { get; set; }

        DataAppDataContext DataAppData;
        public Authenticity()
        {
            DataAppData = new DataAppDataContext();
        }
        public bool IsValid(string UserEmail, string PassWord)
        {

            var Accout = DataAppData.Accounts.SingleOrDefault(q => q.Equals(UserEmail) && q.Equals(HasdMD5.GetMD5Hash(PassWord)));
            if(Accout.AccountID != 0)
                return true;
            else
                return false;
        }

        public bool IsValid(Authenticity authenticity)
        {
            if (authenticity == null)
                return false;
            if (!string.IsNullOrEmpty(authenticity.Token))
                return (HttpRuntime.Cache[authenticity.Token] != null);
            
                return false;
        }
    }
}
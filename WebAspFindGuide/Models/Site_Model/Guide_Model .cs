using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAspFindGuide.Models.Site_Model
{
    public class Guide_Model
    {
        WebData webData;
        private Guide_Model()
        {
            webData = new WebData();
        }
        protected static Guide_Model instance;
        public static Guide_Model Instance
        {
            get => { if (instance == null) return new Guide_Model(); else return instance; }
            set => instance = value;
        }

        public List<Account> GetListGuide()
        {
            var list = webData.Accounts.ToList();
            foreach (var guide in list)
            {
                guide.Account_Pass = String.Empty;
            }

            return list;
        } 
        public List<Account> GetListGuideByAreaID(int AreaID)
        {
            var list = webData.Accounts.Where(q => q.Account_Area == AreaID).ToList();
            foreach(var guide in list)
            {
                guide.Account_Pass = String.Empty;
            }
            
            return list;
        }
        public Account GetGuideByID(string ID)
        {
            var guide = webData.Accounts.Where(q => q.AccountID == ID).SingleOrDefault();
            guide.Account_Pass = String.Empty;
            return guide;
        }
    }
}
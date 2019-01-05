
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebAspFindGuide.Models.Site_Model
{
    public class Guide_Model
    {
        WebData webData;
        private Guide_Model ()
        {
            webData = new WebData();

        }
        protected static Guide_Model instance;
        public static Guide_Model Instance
        {
            get { if (instance == null) return new Guide_Model(); else return instance; }
            set { instance = value; }
        }

        public List<CustomAccount> GetAllGuide()
        {
            var list = webData.Accounts.ToList();
            List<CustomAccount> listGuide = new List<CustomAccount>();
            foreach (var acc in list)
            {
                listGuide.Add(new CustomAccount
                {
                    AccountID = acc.AccountID,
                    Account_Email = acc.Account_Email,
                    Account_Name = acc.Account_Name,
                    Account_Area = acc.Account_Area,
                    Acccount_Point = acc.Acccount_Point,
                    Account_Address = acc.Account_Address,
                    Account_Avarta = acc.Account_Avarta,
                    Account_Phone = acc.Account_Phone,
                    Account_Info_more = acc.Account_Info_more,
                    Account_Gender = acc.Account_Gender,
                    Account_Info_Schedule = acc.Account_Info_Schedule,
                    Accout_Image_more = acc.Accout_Image_more

                });
            }
            return listGuide;
        }
        public CustomAccount GetGuideByID(string id)
        {
            var acc = webData.Accounts.SingleOrDefault(q => q.AccountID == id);          
            return new CustomAccount
                {
                AccountID = acc.AccountID,
                    Account_Email = acc.Account_Email,
                    Account_Name = acc.Account_Name,
                    Account_Area = acc.Account_Area,
                    Acccount_Point = acc.Acccount_Point,
                    Account_Address = acc.Account_Address,
                    Account_Avarta = acc.Account_Avarta,
                    Account_Phone = acc.Account_Phone,
                    Account_Info_more = acc.Account_Info_more,
                    Account_Gender = acc.Account_Gender,
                    Account_Info_Schedule = acc.Account_Info_Schedule,
                    Accout_Image_more = acc.Accout_Image_more

                };

        }
        public List<CustomAccount> GetGuideByAreaID(int AreaID)
        {
            var list = webData.Accounts.Where(q => q.Account_Area == AreaID).ToList();
            List<CustomAccount> listGuide = new List<CustomAccount>();
            foreach (var acc in list)
            {
                listGuide.Add(new CustomAccount
                {
                    AccountID = acc.AccountID,
                    Account_Email = acc.Account_Email,
                    Account_Name = acc.Account_Name,
                    Account_Area = acc.Account_Area,
                    Acccount_Point = acc.Acccount_Point,
                    Account_Address = acc.Account_Address,
                    Account_Avarta = acc.Account_Avarta,
                    Account_Phone = acc.Account_Phone,
                    Account_Info_more = acc.Account_Info_more,
                    Account_Gender = acc.Account_Gender,
                    Account_Info_Schedule = acc.Account_Info_Schedule,
                    Accout_Image_more = acc.Accout_Image_more

                });
            }
            return listGuide;

        }
        public List<CustomAccount> GetGuideOrdePonit(int soLuong)
        {
            var list = webData.Accounts.OrderBy(q=>q.Acccount_Point).Take(soLuong).ToList();
            List<CustomAccount> listGuide = new List<CustomAccount>();
            foreach (var acc in list)
            {
                listGuide.Add(new CustomAccount
                {
                    AccountID = acc.AccountID,
                    Account_Email = acc.Account_Email,
                    Account_Name = acc.Account_Name,
                    Account_Area = acc.Account_Area,
                    Acccount_Point = acc.Acccount_Point,
                    Account_Address = acc.Account_Address,
                    Account_Avarta = acc.Account_Avarta,
                    Account_Phone = acc.Account_Phone,
                    Account_Info_more = acc.Account_Info_more,
                    Account_Gender = acc.Account_Gender,
                    Account_Info_Schedule = acc.Account_Info_Schedule,
                    Accout_Image_more = acc.Accout_Image_more

                });
            }
            return listGuide;
        }

        public bool SaveSchedule(string id,string Schedule)
        {
            try
            {
                var acc = webData.Accounts.SingleOrDefault(q => q.AccountID == id);
                acc.Account_Info_Schedule = Schedule;
                webData.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebAspFindGuide.Models.Site_Model
{
    public class Account_Model
    {
        private static Account_Model instance;
        public static Account_Model Instance
        {
          set
            {
                instance = value; 
            }   
            get
            {
                if (instance == null)
                    return new Account_Model();
                else return instance;
            }
        }
        WebData webData;
        public Account_Model()
        {
            webData = new WebData();
        }
       
      
      
        public string[] GetRoleAccountByUserName(string usename)
        {

            try
            {
                var RoleID = webData.Accounts.Where(q => q.Account_Name == usename).SingleOrDefault().Account_RoleID;
                var Role = (from r in webData.Roles where r.RoleID == RoleID select r.RoleKey).ToArray();
                return Role;
                /* webData.Roles.Where(r => r.RoleID == (webData.Accounts.Where(q => q.AccountID == id).SingleOrDefault().Account_RoleID));*/
            }
            catch (Exception)
            {
                return null; 
            }
        }
       
        public Account GetAccountByUserName(string username)
        {

            try
            {

                return webData.Accounts.SingleOrDefault(q => q.Account_Name == username);

            }
            catch (Exception)
            {
                return null;
            }


        }
        /// <summary>
        /// trả về số ID khác các trường hợp dưới đăng nhập thành công
        ///        0 không tồn tại tài khoản này
        ///        -1 mật khẩu không đúng
        ///        -2 chưa dực config
        ///        -3 tài khoản bị khóa
        ///        -4 lỗi hệ thống    
        /// </summary>
        /// <param name="EmailOrPhone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
      
        public string ValidateUser(string Email, string pass)
        {

           
            try
            {
                var account = webData.Accounts.SingleOrDefault(q => q.Account_Email.ToUpper() == Email.ToUpper());
                if (account != null)
                {
                    if (account.Account_Pass == Common.HashMD5.GetMD5Hash(pass))
                    {
                        if (account.Account_Config == true)
                        {
                            if (account.Account_Lock == false)
                                return account.AccountID;
                            else return "-3";
                        }
                        else
                            return "-2";

                    }
                    else
                        return "-1";

                }
                else
                    return "0";

            }
            catch (Exception)
            {
                return "-4";
            }



        }
        /// <summary>
        /// lấy tài khoản không bao gồm pass theo id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Account GetAccountByID(string ID)
        {
           
            var account = webData.Accounts.SingleOrDefault(q => q.AccountID == ID);
            account.Account_Pass = "";

            return account;
        }

     
        public bool CreateAccount([Bind(Include = "Account_Email,Account_Name,Account_Pass,Account_Phone,Account_CreateDate,Account_CodeConfig")]Account new_account)
        {
            try
            {
                
                webData.Accounts.Add(new_account);
                webData.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Thông tin trong tài khoản cũ được thay thế hoàn toàn 
        /// </summary>
        /// <param name="new_account"></param>
        /// <returns></returns>
       
        public bool UpDateAccount(Account new_account)
        {
            try
            {
                var account = webData.Accounts.SingleOrDefault(q => q.AccountID == new_account.AccountID);
                account = new_account;
                webData.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActivationAccount(string code)
        {
            try
            {

                var account = webData.Accounts.Where(u => u.Account_CodeConfig.ToString().Equals(code)).FirstOrDefault();
                account.Account_Config = true;
                webData.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        public bool Exist_EmailOrPhone(string EmailOrPhone)
        {
           
            try
            {
                var account = webData.Accounts.Where(q => q.Account_Email == EmailOrPhone || q.Account_Phone == EmailOrPhone).SingleOrDefault();
                if (account != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
       
             public string GetRoleAccountByRoleID(int? RoleID)
        {
           
            try
            {
               
                var Role = webData.Roles.Where(q=>q.RoleID == RoleID).First();
                return Role.RoleKey;
                /* webData.Roles.Where(r => r.RoleID == (webData.Accounts.Where(q => q.AccountID == id).SingleOrDefault().Account_RoleID));*/
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
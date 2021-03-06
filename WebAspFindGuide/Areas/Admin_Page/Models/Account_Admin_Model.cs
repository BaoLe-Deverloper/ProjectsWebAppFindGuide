﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAspFindGuide.Areas.Admin_Page.Models.Objects;
using WebAspFindGuide.Models;

namespace WebAspFindGuide.Areas.Admin_Page.Models
{
    public class Account_Admin_Model
    {
        private static Account_Admin_Model instance;
        public static Account_Admin_Model Instance
        {
            set
            {
                instance = value;
            }
            get
            {
                if (instance == null)
                    return new Account_Admin_Model();
                else return instance;
            }
        }
        WebData webData;
        public Account_Admin_Model()
        {
            webData = new WebData();
        }


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
                            {
                                if (account.Account_RoleID == 3)
                                    return "OK";
                                else
                                    return "You do not have sufficient permissions to access this page !";
                            }

                            else return "This account is locked !";
                        }
                        else
                            return "You have not confirmed Email !";

                    }
                    else
                        return "wrong password !";

                }
                else
                    return "This email does not exist !";

            }
            catch (Exception)
            {
                return "Login failed !";
            }
        }

        public Admin_Account GetAccountByEmail(string email)
        {
            var acc = webData.Accounts.SingleOrDefault(q => q.Account_Email.ToUpper() == email.ToUpper());
            return new Admin_Account
            {
                AccountID = acc.AccountID,
                Account_Email = acc.Account_Email,
                Account_Avarta = acc.Account_Avarta,
                Account_CreateDate = acc.Account_CreateDate,
                Account_Name = acc.Account_Name,
                Account_Gender = acc.Account_Gender,
                Account_Phone = acc.Account_Phone
            };
        }
        public bool ExistEmail(string email)
        {
            var acc = webData.Accounts.SingleOrDefault(q => q.Account_Email.ToUpper() == email.ToUpper());
            if (acc != null)
                return true;
            else return false;
        }

        public async Task<bool> ActivationAccount(string code)
        {
            try
            {

                var account = webData.Accounts.Where(u => u.Account_CodeConfig.ToString().Equals(code)).FirstOrDefault();
                account.Account_Config = true;
                await webData.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateAccount([Bind(Include = "Account_Email,Account_Name,Account_RoleID,Account_Pass,Account_Phone,Account_CreateDate,Account_CodeConfig")]Account new_account)
        {
            try
            {
                webData.Accounts.Add(new_account);
                await webData.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ChangePass(string ID, string email, string oldpass, string newpass)
        {

            try
            {
                var account = webData.Accounts.SingleOrDefault(q => q.AccountID == ID || q.Account_Email.ToUpper() == email.ToUpper());
                if (account != null && account.Account_Pass == Common.HashMD5.GetMD5Hash(oldpass))
                {
                    account.Account_Pass = Common.HashMD5.GetMD5Hash(newpass);
                    await webData.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
          
        public string GetAccountCodeConfig(string email)
        {
            var account = webData.Accounts.SingleOrDefault(q => q.Account_Email.ToUpper() == email.ToUpper());
            return account.Account_CodeConfig;
        }

        public async void UnConfirmAccountByEmailOrID(string ID = null, string email = null)
        {
            try
            {

                var account = webData.Accounts.SingleOrDefault(q => q.AccountID == ID || q.Account_Email.ToUpper() == email.ToUpper());
                account.Account_Config = false;
                await webData.SaveChangesAsync();

            }
            catch(Exception)
            {

            }
        }
      
        public async Task<bool> LockAccount(string email)
        {
            try
            {

                var account = webData.Accounts.SingleOrDefault(q => q.Account_Email.ToUpper() == email.ToUpper());
                account.Account_Lock = true;
                await webData.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UnLockAccount(string email)
        {
            try
            {

                var account = webData.Accounts.SingleOrDefault(q => q.Account_Email.ToUpper() == email.ToUpper());
                account.Account_Lock = false;
                await webData.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
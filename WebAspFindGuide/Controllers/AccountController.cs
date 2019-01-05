using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAspFindGuide.Common;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers
{

    public class AccountController : Controller
    {
        Account_Model account_Model;
        public AccountController()
        {
            account_Model = Account_Model.Instance;
        }
     
        [HttpGet]
  
        public ActionResult LogIn()
        {
            LogOut();
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogIn(string email, string pass, string remember)
        {
          
            var Validate = account_Model.ValidateUser(email, pass);
            switch (Validate)
            {
                case "-4":
                    ViewBag.Error = "System error !";
                    break;
                case "-3":
                    ViewBag.Error = "This account is locked !";
                    break;
                case "-2":
                    ViewBag.Error = "You have not confirmed Email !";
                    break;
                case "-1":
                    ViewBag.Error = "wrong password !";
                    break;
                case "0":
                    ViewBag.Error = "This email does not exist !";
                    break;
                default:
                    {


                        Account Acc = (account_Model.GetAccountByID(Validate));
                        Acc.Account_Pass = String.Empty;
                       
                       CustomAccount account = new CustomAccount
                       { 

                               AccountID= Acc.AccountID,
                               Account_Name= Acc.Account_Name,
                               Account_Email= Acc.Account_Email,
                               Account_RoleID= Acc.Account_RoleID,
                               Account_Avarta=Acc.Account_Avarta
                           };

                        Session[Common.Const.Session_Account] = account;

                        if (remember == "on")
                        {

                            ////Tao cookie su dung System.Web.Security
                           
                            string userData = JsonConvert.SerializeObject(account);
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                                (
                                1, Acc.Account_Name, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                                );

                            string enTicket = FormsAuthentication.Encrypt(authTicket);

                            HttpCookie user_cookie = new HttpCookie(Common.Const.Cookie_User, enTicket);
                            user_cookie.Expires.AddYears(1);
                            user_cookie.HttpOnly = true;
                            HttpContext.Response.SetCookie(user_cookie);
                        }

                        return RedirectToAction("Index", "Home");
                    }
            }

            return View();

        }

        public ActionResult ActivationAccount(string id)
        {
            var statusAccount = account_Model.ActivationAccount(id);
            if(statusAccount)
            {
                TempData["Success"] = "You have successfully activated your account !";
                return RedirectToAction("LogIn", "Account");
            }
            return RedirectToAction("Page_Error_400", "Page_Errors");
        }

        [NonAction]
        public  void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            string subject = "Activation Account !";
            string body = "<br/> Please click on the following link in order to activate your account<br/>" + "<a href='" + link + "'> Activation Account ! </a>";
            Code.Instance.SendEmail(email, subject, body);

        }
        [HttpGet]
     
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(string name, string email, string phone, string pass)
        {

            try
            {
                if (account_Model.Exist_EmailOrPhone(email))
                {
                    ViewBag.Error = "Error: Email already Exists";
                    return View();
                }
                if (account_Model.Exist_EmailOrPhone(phone))
                {
                    ViewBag.Error = "Error:  Phone Number already Exists";
                    return View();
                }
                Account account = new Account()
                {
                    AccountID = Code.Instance.generateID(),
                    Account_Name = name,
                    Account_Email = email,
                    Account_Phone = phone,
                    Account_Pass = Common.HashMD5.GetMD5Hash(pass),
                    Account_Lock = false,
                    Account_RoleID = 1,
                    Account_CreateDate = DateTime.Now,
                    Account_CodeConfig = Guid.NewGuid().ToString()
                };
                //Save User Data 
                bool re = account_Model.CreateAccount(account);
                if (re)
                {
                    //Verification Email
                    VerificationEmail(account.Account_Email, account.Account_CodeConfig);
                    ViewBag.Success = "Your account has been created successfully.We sent you a confirmation email with a link to activate your subscription. Please check your email and click the link. ^_^";
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Error = "Error: Create account failed!";
                return View();
            }
            catch(Exception)
            {
                ViewBag.Error = "Something Wrong!";
                return View();
            }
          
        }

      

        public ActionResult LogOut()
        {

            Session.RemoveAll();

            if (HttpContext.Response.Cookies[Common.Const.Cookie_User] != null)
            {
                HttpContext.Response.Cookies[Common.Const.Cookie_User].Value = null;
                HttpContext.Response.Cookies[Common.Const.Cookie_User].Expires = DateTime.Now.AddYears(-1);
            }
            return RedirectToAction("Index", "Home");
        }
       
    }
}
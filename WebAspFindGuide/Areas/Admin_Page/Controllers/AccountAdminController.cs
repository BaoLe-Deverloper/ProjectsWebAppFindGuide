using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAspFindGuide.Areas.Admin_Page.Models;
using WebAspFindGuide.Common;
using WebAspFindGuide.Models;

namespace WebAspFindGuide.Areas.Admin_Page.Controllers
{
    public class AccountAdminController : Controller
    {
        // GET: Admin_Page/AccountAdmin
        [HttpGet]

        public ActionResult Login()
        {
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string pass, string remember)
        {
            var Result = Account_Admin_Model.Instance.ValidateUser(email, pass);
            if(Result == "OK")
            {
                var account = Account_Admin_Model.Instance.GetAccountByEmail(email);
                Session[Common.Const.Session_Admin] = account;

                if (remember == "on")
                {
                  
                    ////Tao cookie su dung System.Web.Security

                    string userData = JsonConvert.SerializeObject(account);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                        (
                        1, account.Account_Name, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                        );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);

                    HttpCookie user_cookie = new HttpCookie(Common.Const.Cookie_Admin, enTicket);
                    user_cookie.Expires.AddMonths(+1);
                    user_cookie.HttpOnly = true;
                    HttpContext.Response.SetCookie(user_cookie);
                }
                return RedirectToAction("index", "HomeAdmin");
            }
            ViewBag.Error = Result;
            return View();
        }
        [HttpGet]
       public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string email,string name, string pass)
        {
            try
            {
                // kiểm tra tồn tại của email
                if (Account_Admin_Model.Instance.ExistEmail(email))
                {
                    ViewBag.Error = "Error: Email already Exists";
                    return View();
                }
              
                Account account = new Account()
                {
                    AccountID = Code.Instance.generateID(),
                    Account_Name = name,
                    Account_Email = email,
                    Account_RoleID = 3,
                    Account_Pass = Common.HashMD5.GetMD5Hash(pass),
                    Account_Lock = true,
                    Account_CreateDate = DateTime.Now,
                    Account_CodeConfig = Guid.NewGuid().ToString()
                };


                //Save User Data 

                bool re = Account_Admin_Model.Instance.CreateAccount(account);
                if (re)
                {
                    //Verification Email
                    VerificationEmail(account.Account_Email, account.Account_CodeConfig);
                    ViewBag.Success = "Your account has been created successfully.We sent you a confirmation email with a link to activate your subscription. Please check your email and click the link.  ^_^";
                    return Redirect("~/Home/index");
                }
                else
                    ViewBag.Error = "Error: Create account failed!";
                return View();
            }

            catch (Exception)
            {

                ViewBag.Error = "Error: Something Wrong!";
                return View();
            }
        
        }

        public ActionResult ActivationAccount(string id)
        {
            var statusAccount = Account_Admin_Model.Instance.ActivationAccount(id);
            if (statusAccount)
            {
              //Nếu kích hoạt thành công thì gửi thông báo về Login success qua TempData
                TempData["Success"] = "You have successfully activated your account !";
                return Redirect("~/Admin_Page/AccountAdmin/Login");
            }
          
            return Redirect("~/Page_Errors/Page_Error_400");
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/AccountAdmin/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            string subject = "Activation Account !";
            string body = "<br/> Please click on the following link in order to activate your account<br/>" + "<a href='" + link + "'> Activation Account ! </a>";
            Code.Instance.SendEmail(email, subject, body);

        }
      
    }
}
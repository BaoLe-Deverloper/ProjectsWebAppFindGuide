using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
                    ModelState.AddModelError("", "Lỗi hệ thống !");
                    break;
                case "-3":
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                    break;
                case "-2":
                    ModelState.AddModelError("", "Tài khoản Chưa được xác minh.");
                    break;
                case "-1":
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                    break;
                case "0":
                    ModelState.AddModelError("", "Không tìm thấy Tài khoản này.");
                    break;
                default:
                    {


                        Account Acc = (account_Model.GetAccountByID(Validate));
                        Acc.Account_Pass = String.Empty;
                       
                        string[] Account = new string[]
                           {

                                Acc.AccountID,
                                Acc.Account_Name,
                                Acc.Account_Email,
                                Acc.Account_RoleID.ToString(),
                                Acc.Account_Avarta
                           };

                        Session[Common.Const.Session_Account] = Account;

                        if (remember == "on")
                        {

                            ////Tao cookie su dung System.Web.Security
                           
                            string userData = JsonConvert.SerializeObject(Account);
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
                ViewBag.Status = "Chúc mừng bạn xác nhận email thành công !";
                return RedirectToAction("LogIn", "Account");
            }
            return RedirectToAction("Page_Error_400", "Page_Errors");
        }

        [NonAction]
        public  void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromEmail = new MailAddress("Baolerussia@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "15081998";
            string subject = "Activation Account !";

            string body = "<br/> Please click on the following link in order to activate your account<br/>" + "<a href='" + link + "'> Activation Account ! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })

             smtp.Send(message);

        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string name, string email, string phone, string pass, string passconfig)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                if (pass != passconfig)
                {
                    ModelState.AddModelError("", "Sorry: PassWord not config");
                    return View();
                }
                if (account_Model.Exist_EmailOrPhone(email))
                {
                    ModelState.AddModelError("", "Sorry: Email already Exists");
                    return View();
                }
                if (account_Model.Exist_EmailOrPhone(phone))
                {
                    ModelState.AddModelError("", "Sorry:  Phone Number already Exists");
                    return View();
                }
                Account account = new Account()
                {
                    AccountID = generateID(),
                    Account_Name = name,
                    Account_Email = email,
                    Account_Phone = phone,
                    Account_Pass = Common.HashMD5.GetMD5Hash(pass),
                    Account_Lock = false,
                    Account_CreateDate = DateTime.Now,
                    Account_CodeConfig = Guid.NewGuid().ToString()
                };

                bool re = account_Model.CreateAccount(account);
                //Save User Data 


                //Verification Email
                VerificationEmail(account.Account_Email, account.Account_CodeConfig);
                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return RedirectToAction("Index","Home");
        }

        public string generateID()
        {
            

            string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000)+ DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString();

            return number;
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
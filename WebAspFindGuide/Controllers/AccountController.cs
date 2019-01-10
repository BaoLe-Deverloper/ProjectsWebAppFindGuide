using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
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
        #region Đăng nhập tài khoản
        [HttpGet]
        public ActionResult LogIn(string ReturnUrl,string Message)
        {
            LogOut();

            ViewBag.ReturnUrl = ReturnUrl;
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];
            if (!String.IsNullOrEmpty(Message))
                ViewBag.Error = Message;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogIn(string email, string pass, string remember, string ReturnUrl)
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
                    {
                        ViewBag.status = true;
                        ViewBag.link = String.Format("ResendActivationEmail?email={0}", email);
                        ViewBag.Error = String.Format("You have not confirmed Email {0}!", email);
                        break;
                    }

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

                            AccountID = Acc.AccountID,
                            Account_Name = Acc.Account_Name,
                            Account_Email = Acc.Account_Email,
                            Account_RoleID = Acc.Account_RoleID,
                            Account_Avarta = Acc.Account_Avarta
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
                        if(String.IsNullOrEmpty(ReturnUrl))
                        {
                            return  RedirectToAction("Index", "Home");
                        }
                        else return  RedirectToLocal(ReturnUrl);
                    }
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();

        }
        #endregion 

        #region Đăng kí tài khoản mới
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(string name, string email, string phone, string pass)
        {
            var response = Captcha.ValidateCaptcha(Request["g-recaptcha-response"]);
            if (!response.Success)
            {
                ViewBag.Error = "Error From Google ReCaptcha : " + response.ErrorMessage[0].ToString();
                return View();
            }
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
                bool re = await account_Model.CreateAccount(account);
                //Verification Email
                var result = await VerificationEmail(account.Account_Email, account.Account_CodeConfig);
                if (re && result)
                {
                    ViewBag.Success = "Your account has been created successfully.We sent you a confirmation email with a link to activate your subscription. Please check your email and click the link. ^_^";
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Error = "Error: Create account failed!";
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "Something Wrong!";
                return View();
            }

        }
        #endregion

        #region Đăng xuất tài khoản

        public ActionResult LogOut()
        {

            Session.Remove(Common.Const.Session_Account);

            if (HttpContext.Response.Cookies[Common.Const.Cookie_User] != null)
            {
                HttpContext.Response.Cookies[Common.Const.Cookie_User].Value = null;
                HttpContext.Response.Cookies[Common.Const.Cookie_User].Expires = DateTime.Now.AddYears(-1);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Kích hoạt email
        public async Task<ActionResult> ActivationAccount(string id)
        {
            var statusAccount = await account_Model.ActivationAccount(id);
            if (statusAccount)
            {
                TempData["Success"] = "You have successfully activated your account !";
                return RedirectToAction("LogIn", "Account");
            }
            return RedirectToAction("Page_Error_404", "Page_Errors");
        }
        [NonAction]
        public async Task<bool> VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            string subject = "Activation Account !";
            string body = "<br/> Please click on the following link in order to activate your account<br/>" + "<a href='" + link + "'> Activation Account ! </a>";
            return await Code.Instance.SendEmail(email, subject, body);

        }
        #endregion

        #region Gửi lại xác nhận enail
        public async Task<ActionResult> ResendActivationEmail(string email)
        {
            var result = await VerificationEmail(email, account_Model.GetAccountCodeConfig(email));
            if (result)
            {
                TempData["Success"] = "We sent you a confirmation email with a link to activate your subscription. Please check your email and click the link!";
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Page_Error_404", "Page_Errors");
        }
        #endregion

        #region Mở khóa và Xác nhận mở khóa qua email
        [HttpGet]
        public  ActionResult UnLockAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UnLockAccount(string email)
        {

            //Thay đổi codeconfirm trong accout đồng thời gửi link chứa code đó đến email
            var code = Guid.NewGuid().ToString();
            if (await account_Model.changeAccountCode(code,null,email)&&await ConfirmUnlock(email,code))
            {

                TempData["Success"] = "Please check your email and click the link! Or enter another account.";
                return RedirectToAction("Login", "Account");
            }
            else
                ViewBag.Error = "System error.";
            return View();
        }

        public async Task<ActionResult> ConfirmUnlock(string id)
        {
            var statusAccount = await account_Model.ConfirmUnlock(id);
            if (statusAccount)
            {
                TempData["Success"] = "You have successfully unlocked your account. Login now !";
                return RedirectToAction("LogIn", "Account");
            }
            return RedirectToAction("Page_Error_404", "Page_Errors");
        }

        [NonAction]
        public async Task<bool> ConfirmUnlock(string email, string activationCode)
        {

            var url = string.Format("/Account/ConfirmUnlock/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            string subject = "Confirm account unlocking !";
            string body = "<br/>Please click on the link to confirm unlocking your account<br/>" + "<a href='" + link + "'>Confirm account unlocking! </a>";
            return await Code.Instance.SendEmail(email, subject, body);

        }
        #endregion

        #region khóa tài khoản
        public async Task<ActionResult> LockAccount(string email = null)
        {
            CustomAccount acc = new CustomAccount();
            if (Session[Common.Const.Session_Account] != null)
            {
                acc = (CustomAccount)Session[Common.Const.Session_Account];
            }
            if (await account_Model.LockAccount(String.IsNullOrEmpty(acc.Account_Email) ? email : acc.Account_Email))
            {
                LogOut();
                TempData["Message"] = "Your account has been locked.";
                return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.Error = "System error.";
            return View();


        }
        #endregion

        #region Quên mật khẩu
        //Quên mật khẩu nếu tồn tại sesion thì lấy id hiện ảnh ẩn ô nhập email 
        [HttpGet]
        public ActionResult ForgotPass()
        {
            if (Session[Const.Session_Account] == null)
            {
                HttpCookie use_cookie = Request.Cookies[Common.Const.Cookie_User];
                if (use_cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(use_cookie.Value);
                    Session[Common.Const.Session_Account] = JsonConvert.DeserializeObject<CustomAccount>(ticket.UserData);
                }
            }
            return View();
        }

        // phương thức nhận nếu remmber on thì tạo codeConfig mới Account_config = false rồi gửi mail xác nhận về cho user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPass(string newpass, string remember, string oldpass = null, string email = null)
        {
            CustomAccount acc = new CustomAccount();
            if (Session[Common.Const.Session_Account] != null)
            {
                acc = (CustomAccount)Session[Common.Const.Session_Account];
            }

            //Nếu quên mật khẩu cũ thì đổi mật khẩu xong gửi mail xác nhận về cho email đó
            if (remember == "on")
            {
                var code = Guid.NewGuid().ToString();
                if (await account_Model.changeAccountCode(code, acc.AccountID, email) && await account_Model.ChangePassNoOldPass(acc.AccountID, email, newpass))
                {

                    if (await ConfirmEmail(String.IsNullOrEmpty(email) ? acc.Account_Email : email, code))
                    {
                        if (await account_Model.UnConfirmAccountByEmailOrID(acc.AccountID, email))
                        {
                            TempData["Success"] = "We sent you a confirmation email with a link to activate your subscription. Please check your email and click the link!";
                            return RedirectToAction("LogIn", "Account");
                        }

                    }
                }
            }
            else
            {
                var result = await account_Model.ChangePass(acc.AccountID, email, oldpass, newpass);
                if (result)
                {
                    TempData["Success"] = "You have successfully changed your account password !";
                    return RedirectToAction("LogIn", "Account");
                }
            }

            ViewBag.Error = "password change failed.";
            return View();
        }
        public async Task<ActionResult> ConfirmEmail(string id)
        {
            var statusAccount = await account_Model.ActivationAccount(id);
            if (statusAccount)
            {
                TempData["Success"] = "You have successfully Confirm password change !";
                return RedirectToAction("LogIn", "Account");
            }
            return RedirectToAction("Page_Error_404", "Page_Errors");
        }
        [NonAction]
        public async Task<bool> ConfirmEmail(string email, string activationCode)
        {

            var url = string.Format("/Account/ConfirmEmail/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            string subject = "Confirm password change !";
            string body = "<br/>Please click on the link to confirm the password change<br/>" + "<a href='" + link + "'> Confirm password change ! </a>";
            return await Code.Instance.SendEmail(email, subject, body);

        }

        #endregion

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
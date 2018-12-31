using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers.WebApi
{
    public class AccountController : ApiController
    {
        Account_Model account_Model;
        public AccountController()
        {
            account_Model = new Account_Model();
        }
        [Route("api/Account/Login")]
        [HttpPost]
        public string Login(string email, string pass)
        {
            return account_Model.ValidateUser(email, pass);

        }
        [Route("api/Account/Create")]
        [HttpPost]
        public bool CreateUser(Account account)
        {
            if (!account_Model.Exist_EmailOrPhone(account.Account_Email))
                return account_Model.CreateAccount(account);
            return false;
        }
        [Route("api/Account/GetGuide")]
        [HttpGet]
        public List<CustomAccount> GetGuide()
        {
            return Guide_Model.Instance.GetAllGuide();
        }
    }
}

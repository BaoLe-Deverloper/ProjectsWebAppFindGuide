using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.Site_Model;

namespace WebAspFindGuide.Controllers.WebApi
{
    public class AccountController : ApiController
    {

        [Route("api/Account/Login")]
        [HttpPost]
        public string Login(string email, string pass)
        {
            return Account_Model.Instance.ValidateUser(email, pass);

        }
        [Route("api/Account/Create")]
        [HttpPost]
        public async Task<string> CreateUser(Account account)
        {
            if (!Account_Model.Instance.Exist_EmailOrPhone(account.Account_Email))
            {
                var re = await Account_Model.Instance.CreateAccount(account);
                if (re == true)
                    return "Success.";
                else return "Error Connect to database.";
            }
               
            return "Error" + account.Account_Email + " Exist !";
        }
        [Route("api/Account/GetAllGuide")]
        [HttpGet]
        public List<CustomAccount> GetAllGuide()
        {
            return Guide_Model.Instance.GetAllGuide();
        }
        [Route("api/Account/GetGuideByID")]
        [HttpGet]
        public async Task<CustomAccount> GetAllGuide(string id)
        {
            return  Guide_Model.Instance.GetGuideByID(id);
        }
    }
}

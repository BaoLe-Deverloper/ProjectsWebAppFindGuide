using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace WebAspFindGuide.Common
{
    public class Code
    {
        private static Code instance;
        public static Code Instance
        {
            get { if (instance == null) return new Code(); else return instance; }
            set { instance = value; }
        }
        public string generateID()
        {
            string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000) + DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString();

            return number;
        }
        public async Task<bool> SendEmail(string email, string subject, string body)
        {
            var fromEmail = new MailAddress(ConfigurationManager.AppSettings["WebEmail"], "WebAppFindeGuide");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = ConfigurationManager.AppSettings["PassEmail"];

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
                try
                {
                    await smtp.SendMailAsync(message);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

        }
    }
}
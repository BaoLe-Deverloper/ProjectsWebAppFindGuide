using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAspFindGuide.Models.SystemModel
{
    public class ChatUserCurrent
    {
        public string UserID { get; set; }
        public string Ip { get; set; }
        public string ConnectoinID { get; set; }
        public DateTime TimeStartConnect { get; set; }
      
    }
}
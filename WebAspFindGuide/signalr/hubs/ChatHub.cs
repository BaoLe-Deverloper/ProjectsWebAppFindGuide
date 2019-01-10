using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebAspFindGuide.CustomAuthencation;
using WebAspFindGuide.Models;
using WebAspFindGuide.Models.SystemModel;

namespace WebAspFindGuide.signalr.hubs
{
    [CustomAttribute(PermissionName = "Client|Admin|Guide")]
    public class ChatHub : Hub
    {
        public static List<ChatUserCurrent> listUsers = new List<ChatUserCurrent>();
        

        public async void SendMessage(string name, string message, string receiverAccountID,  string receiverConnectID)
        {
            if (!String.IsNullOrEmpty(message))
            {
                var TimeSend = DateTime.Now;
                var username = Context.User.Identity.Name;
                Message chat = new Message()
                {
                    Message_AccountID_From = listUsers.SingleOrDefault(q=>q.ConnectoinID == GetClientId()).UserID,
                    Message_AccountID_To= receiverAccountID,
                    Message_Content= message,
                    Message_TimeToSend = TimeSend
                };
                 await System_Model.Instance.InsertMessage(chat);
                if (String.IsNullOrEmpty(receiverConnectID))
                {
                    Clients.Caller.broadcastMessage(TimeSend.ToString(),name, message);
                }
                Clients.Clients(new string[] { Context.ConnectionId, receiverConnectID }).broadcastMessage(TimeSend.ToString(), name, message);
            }
           
        }
        public void AddUser(string SessionUser)
        {
            ChatUserCurrent data = new ChatUserCurrent()
            {
                Ip = HttpContext.Current.Request.UserHostAddress,
                TimeStartConnect = DateTime.Now,
                UserID = SessionUser,
                ConnectoinID = GetClientId()
            };
            if (listUsers.SingleOrDefault(q => q.ConnectoinID == GetClientId()) == null)
            {
                listUsers.Add(data);
                Clients.All.usersConnected(listUsers);
            }

        }
        public override Task OnConnected()
        {

            return base.OnConnected();

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            listUsers.Remove(listUsers.SingleOrDefault(q => q.ConnectoinID == GetClientId()));
            Clients.All.usersConnected(listUsers);
            return base.OnDisconnected(stopCalled);
        }


        private string GetClientId()
        {
            string clientId = "";
            if (Context.QueryString["clientId"] != null)
            {
                // clientId passed from application 
                clientId = this.Context.QueryString["clientId"];
            }

            if (string.IsNullOrEmpty(clientId.Trim()))
            {
                clientId = Context.ConnectionId;
            }

            return clientId;
        }
    }
}
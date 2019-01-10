using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAspFindGuide.Models.SystemModel
{
    public class System_Model
    {
        private static System_Model instance;
        public static System_Model Instance
        {
            set
            {
                instance = value;
            }
            get
            {
                if (instance == null)
                    return new System_Model();
                else return instance;
            }
        }
        WebData webData;
        public System_Model()
        {
            webData = new WebData();
        }

        public async Task<bool> InsertMessage (Message message)
        {
            try
            {
                webData.Messages.Add(message);
                await webData.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Message> GetAllMessagesByID(string formID,string toID)
        {
            try
            {
                return webData.Messages.Where(q => q.Message_AccountID_From == formID && q.Message_AccountID_To == toID).OrderByDescending(t=>t.Message_TimeToSend).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<Message> GetAllMessagesByIDNotRead(string formID, string toID)
        {
            try
            {
                return webData.Messages.Where(q => q.Message_AccountID_From == formID && q.Message_AccountID_To == toID && q.Message_Status==false).OrderByDescending(t => t.Message_TimeToSend).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}
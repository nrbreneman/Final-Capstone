using System.ComponentModel.DataAnnotations;

namespace WebApplication.Web.Models.Messages
{
    public class MessagesModel
    {
        public int ID { get; set; }
        public int SentByID { get; set; }
        public int SentToID { get; set; }
        public string MessageBody { get; set; }
    }
}
namespace SportsClubOrganizer.Web.Models.Messages
{
    public class MessagesModel
    {
        public int ID { get; set; }
        public int SentByID { get; set; }
        public string SentByName { get; set; }
        public int SentToID { get; set; }
        public string MessageBody { get; set; }

        public string MyTeamName { get; set; }
        public string HomeOrAway { get; set; }
        public string ProposedDates { get; set; }

        public string UserAccepted { get; set; }
    }
}
using System;

namespace SportsClubOrganizer.Web.Models
{
    public class Game
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
    }
}
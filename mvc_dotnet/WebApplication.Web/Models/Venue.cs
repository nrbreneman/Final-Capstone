using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClubOrganizer.Web.Models
{
    public class Venue
    {
        public string PrimaryVenueName { get; set; }
        public string SecondaryVenueName { get; set; }
        public string SelectedVenueName { get; set; }
        public bool IsHome { get; set; }
        public Team Team { get; set; }
    }
}

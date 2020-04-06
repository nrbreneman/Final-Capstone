using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string League { get; set; }
        public string Org { get; set; }
        public string PrimaryVenue { get; set; }
        public string SecondaryVenue { get; set; }
        public List<DateTime> HomeDates { get; set; } = new List<DateTime>();
        public List<DateTime> TravelDates { get; set; } = new List<DateTime>();
    }
}

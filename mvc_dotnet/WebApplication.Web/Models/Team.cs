using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Team
    {
        public int TeamID { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "League")]
        public string League { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public string Org { get; set; }

        [Required]
        [Display(Name = "Primary Venue")]
        public string PrimaryVenue { get; set; }

        
        [Display(Name = "Secondary Venue")]
        public string SecondaryVenue { get; set; }

        [Display(Name = "Available Home Dates")]
        public List<DateTime> HomeDates { get; set; } = new List<DateTime>();

        [Display(Name = "Available Travel Dates")]
        public List<DateTime> TravelDates { get; set; } = new List<DateTime>();
    }
}

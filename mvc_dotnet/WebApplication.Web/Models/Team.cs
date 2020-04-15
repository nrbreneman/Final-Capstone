using Microsoft.AspNetCore.Mvc.Rendering;
using SportsClubOrganizer.Web.Models.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsClubOrganizer.Web.Models
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

        [Display(Name = "Home Date")]
        [DataType(DataType.Date)]
        public DateTime? HomeDate { get; set; }

        [Display(Name = "Travel Date")]
        [DataType(DataType.Date)]
        public DateTime? TravelDate { get; set; }

        [Display(Name = "Available Home Dates")]
        [DataType(DataType.Date)]
        public List<DateTime?> HomeDates { get; set; } = new List<DateTime?>();

        [Display(Name = "Available Travel Dates")]
        [DataType(DataType.Date)]
        public List<DateTime?> TravelDates { get; set; } = new List<DateTime?>();

        public int UserID { get; set; }

        public IList<SelectListItem> DropDownListTeam = new List<SelectListItem>();
        public IList<SelectListItem> LeagueDropDown = new List<SelectListItem>();

        public List<MessagesModel> Messages { get; set; }

        public string SelectedVenue { get; set; }

        public string Message { get; set; }

        //public string TeamNameSort { get; set; }
        //public string HomeDateSort { get; set; }
        //public string TravelDateSort { get; set; }
        //public string TimesPlayedSort { get; set; }
        //public string CurrentFilter { get; set; }
        //public string CurrentSort { get; set; }
        //public string Primary { get; set; }
        //public string Secondary { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        public List<DateTime> HomeDates { get; set; } = new List<DateTime>();

        [Display(Name = "Available Travel Dates")]
        [DataType(DataType.Date)]
        public List<DateTime> TravelDates { get; set; } = new List<DateTime>();

        public int UserID { get; set; }

        public IList<SelectListItem> DropDownListTeam = new List<SelectListItem>();
        public IList<SelectListItem> LeagueDropDown = new List<SelectListItem>();

        [Display(Name = "Home Date")]
        [DataType(DataType.Date)]
        public DateTime? HomeDate { get; set; }

        [Display(Name = "Travel Date")]
        [DataType(DataType.Date)]
        public DateTime? TravelDate { get; set; }

        //public void AddHomeDate(DateTime HomeDate)
        //{
        //    HomeDates.Add(HomeDate);
        //}
        //public void AddTravelDate(DateTime TravelDate)
        //{
        //    TravelDates.Add(TravelDate);
        //}
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL.Models
{
    public class EmpModel
    {
        [Display(Name = "Home Date")]
        [DataType(DataType.Date)]
        public DateTime? HomeDate { get; set; }

        [Display(Name = "Travel Date")]
        [DataType(DataType.Date)]
        public DateTime? TravelDate { get; set; }

        [DataType(DataType.Date)]
        public List<DateTime?> HomeDates { get; set; }

        [DataType(DataType.Date)]
        public List<DateTime?> TravelDates { get; set; }

        //Added
        public IList<SelectListItem> DropDownListTeam = new List<SelectListItem>();

        public string TeamName { get; set; }

        public int? TeamID { get; set; }

        public User User { get; set; }
        //end
    }
}
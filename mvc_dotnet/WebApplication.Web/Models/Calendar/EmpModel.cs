using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}
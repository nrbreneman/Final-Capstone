using System;
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
    }
}
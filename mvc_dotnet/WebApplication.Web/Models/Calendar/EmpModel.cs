using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Web.DAL.Models
{
    public class EmpModel
    {
        /// <summary>
        /// DOB datetime data type property
        /// to display date type control
        /// </summary>
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? HomeDate { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? TravelDate { get; set; }
    }
}
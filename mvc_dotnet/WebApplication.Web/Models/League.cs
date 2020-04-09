using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class League
    {
        public int ID { get; set; }

        [Required]
        public string LeagueName { get; set; }

        [Required]
        public string Org { get; set; }

        [Required]
        public string Sport { get; set; }
    }
}

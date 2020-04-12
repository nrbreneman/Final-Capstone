using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsClubOrganizer.Web.Models
{
    public class League
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("League Name")]
        public string LeagueName { get; set; }

        [Required]
        [DisplayName("Organization")]
        public string Org { get; set; }

        [Required]
        public string Sport { get; set; }
    }
}
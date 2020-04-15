using System.ComponentModel.DataAnnotations;

namespace SportsClubOrganizer.Web.Models
{
    public class Player
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        //public string formatPhone(string PhoneNumer)
        //{
        //    string format;
        //    format.Format(PhoneNumber, "###-###-####");
        //    return format;
        //}
    }
}
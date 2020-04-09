using System.ComponentModel.DataAnnotations;

namespace WebApplication.Web.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string NewPassword { get; set; }

        //[Required]
        public string Salt { get; set; }

        public string Role { get; set; }

        public int TeamID { get; set; }
        public Team UserTeam { get; set; }

        public bool IsAdmin { get; set; }
    }
}
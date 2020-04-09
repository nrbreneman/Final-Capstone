using System.ComponentModel.DataAnnotations;

namespace WebApplication.Web.Models
{
    public class UserViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Salt { get; set; }

        public string Role { get; set; }

        public int TeamID { get; set; }
        public Team UserTeam { get; set; }

        public bool IsAdmin { get; set; }
    }
}
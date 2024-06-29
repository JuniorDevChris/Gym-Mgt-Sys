using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

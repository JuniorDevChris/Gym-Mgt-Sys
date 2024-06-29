using System.ComponentModel.DataAnnotations;

namespace Gym_Client_Mgt_System.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name = "Username")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class LoginView
    {
        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

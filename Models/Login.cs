using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

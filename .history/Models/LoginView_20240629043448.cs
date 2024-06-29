using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class LoginView
    {
        The name 'keyframes' does not exist in the current contextCS0103

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int FailedLoginAttempts { get; set; }
        public bool IsLockedOut { get; set; }
    }
}

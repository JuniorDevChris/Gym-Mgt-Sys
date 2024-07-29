
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int FailedLoginAttempts { get; set; }
        public bool IsLockedOut { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Member>? Members { get; set; }
    }
}

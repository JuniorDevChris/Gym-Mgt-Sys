using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public string? Gender { get; set; }
    }
}

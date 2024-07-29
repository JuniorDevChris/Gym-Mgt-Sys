using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Gym_Mgt_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public override string UserName { get; set; }
    }
}

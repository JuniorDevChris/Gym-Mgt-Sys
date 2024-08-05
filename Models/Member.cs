using System;
using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }

        [Required]
        public int MembershipPlanId { get; set; } // Foreign key to MembershipPlan entity
        public MembershipPlan MembershipPlan { get; set; } // Navigation property

        public int? TrainerId { get; set; } // Nullable foreign key to Trainer entity
        public Trainer Trainer { get; set; } // Navigation property

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        public string Status { get; set; } 
    }
}

using System.Collections.Generic;
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
        public string MembershipPlan { get; set; } // foreign key to a MembershipPlan entity
        public string Trainer { get; set; } // foreign key to a Trainer entity
        public string PhoneNumber { get; set; }
        public string Status { get; set; } 
    }
}




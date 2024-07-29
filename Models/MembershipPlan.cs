using System.ComponentModel.DataAnnotations;

namespace Gym_Mgt_System.Models
{
    public class MembershipPlan
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Required]
        public string PlanType { get; set; } // Subscription or PayAsYouGo
    }
}

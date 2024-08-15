using System.ComponentModel.DataAnnotations;
using Gym_Mgt_System.Models;

namespace Gym_Mgt_System.ViewModels
{
    public class TrainerViewModel : Trainer
    {
        public IEnumerable<Trainer>? Trainers { get; set; } // For the table
    }
}


using System.ComponentModel.DataAnnotations;
using Gym_Mgt_System.Models;

namespace Gym_Mgt_System.ViewModels
{
    public class EmployeeViewModel : ApplicationUser
    {
        [DataType(DataType.Password)]
        public string? Password { get; set; } // Nullable - optional when updating an employee

        public IEnumerable<ApplicationUser>? Employees { get; set; }
    }
}


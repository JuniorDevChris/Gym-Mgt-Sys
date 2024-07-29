using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Models;
using System.Linq;
using Gym_Mgt_System.Data;

namespace Gym_Mgt_System.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", PhoneNumber = "123-456-7890", Email = "jDoe@gym.com", UserName = "jDoe7980", Password = "P@ssw0rd"},
                new Employee { Id = 2, Name = "Jane Smith", PhoneNumber = "234-567-8901", Email = "jSmith@gym.com", UserName = "jSmith8901", Password = "P@ssw0rd"}
            };

            ViewBag.Employees = employees;
            return View("~/Views/Admin/Employees.cshtml");
        }

        [HttpPost]
        public IActionResult CreateOrDeleteEmployee(Employee employee, string action)
        {
            if (action == "Add")
            {
                if (ModelState.IsValid)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (action == "Delete")
            {
                var existingEmployee = _context.Employees.Find(employee.Id);
                if (existingEmployee != null)
                {
                    _context.Employees.Remove(existingEmployee);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Index", _context.Employees.ToList());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Models;
using Gym_Mgt_System.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gym_Mgt_System.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(UserManager<ApplicationUser> userManager, ILogger<EmployeeController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
            var viewModel = new EmployeeViewModel
            {
                Employees = employees
            };
            return View("~/Views/Admin/Employees.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel viewModel)
        {
            // Check if the model is valid according to the validation rules specified
            if (ModelState.IsValid)
            {
                ApplicationUser employee;

                // If the viewModel.Id is not empty, it means we are trying to update an existing employee
                if (!string.IsNullOrEmpty(viewModel.Id))
                {
                    // Find the employee in the database using the Id
                    employee = await _userManager.FindByIdAsync(viewModel.Id);

                    // If the employee is not found, we treat this as a new employee creation
                    if (employee == null)
                    {
                        // If the password is not provided, add a model error and return the view
                        if (string.IsNullOrEmpty(viewModel.Password))
                        {
                            ModelState.AddModelError("Password", "The Password field is required.");
                            return View("~/Views/Admin/Employees.cshtml", viewModel);
                        }

                        // Create a new employee object with the data from the viewModel
                        employee = new EmployeeViewModel
                        {
                            UserName = viewModel.UserName,
                            Email = viewModel.Email,
                            PhoneNumber = viewModel.PhoneNumber,
                            Name = viewModel.Name,
                            Gender = viewModel.Gender
                        };

                        // Attempt to create the new employee with the provided password
                        var res = await _userManager.CreateAsync(employee, viewModel.Password);

                        // If the creation fails, add errors to the model state and return the view
                        if (!res.Succeeded)
                        {
                            foreach (var error in res.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return RedirectToAction(nameof(GetEmployees));
                        }

                        // Add the newly created employee to the "Employee" role
                        await _userManager.AddToRoleAsync(employee, "Employee");
                        return RedirectToAction(nameof(GetEmployees));
                    }

                    // If employee is found, update its properties with the values from the viewModel
                    employee.UserName = viewModel.UserName;
                    employee.Email = viewModel.Email;
                    employee.PhoneNumber = viewModel.PhoneNumber;
                    employee.Name = viewModel.Name;
                    employee.Gender = viewModel.Gender;

                    // Attempt to update the existing employee
                    var result = await _userManager.UpdateAsync(employee);

                    // If the update fails, add errors to the model state and return the view
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
                        return View("~/Views/Admin/Employees.cshtml", viewModel);
                    }

                    // If a password is provided, reset the password for the employee
                    if (!string.IsNullOrEmpty(viewModel.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(employee);
                        var passwordResult = await _userManager.ResetPasswordAsync(employee, token, viewModel.Password);

                        // If the password reset fails, add errors to the model state and return the view
                        if (!passwordResult.Succeeded)
                        {
                            foreach (var error in passwordResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }

                            viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
                            return View("~/Views/Admin/Employees.cshtml", viewModel);
                        }
                    }
                    else
                    {
                        // If no password is provided, just return the updated list of employees
                        viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
                        return View("~/Views/Admin/Employees.cshtml", viewModel);
                    }
                }
                return RedirectToAction(nameof(GetEmployees));
            }

            // If the model state is not valid, log the validation errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogError($"Property: {state.Key} Error: {error.ErrorMessage}");
                }
            }

            // Return the view with the list of employees and validation errors
            viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
            return View("~/Views/Admin/Employees.cshtml", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name
            };
            return View("~/Views/Admin/DeleteEmployee.cshtml", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(employee);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("DeleteEmployee", new EmployeeViewModel { Id = id, Name = employee.Name });
            }

            return RedirectToAction(nameof(GetEmployees));
        }
    }
}

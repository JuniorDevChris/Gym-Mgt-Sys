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
            if (ModelState.IsValid)
            {
                ApplicationUser employee;

                if (!string.IsNullOrEmpty(viewModel.Id))
                {
                    employee = await _userManager.FindByIdAsync(viewModel.Id);
                    if (employee == null)
                    {
                        if(string.IsNullOrEmpty(viewModel.Password))
                        {
                            ModelState.AddModelError("Password", "The Password field is required.");
                            return View("~/Views/Admin/Employees.cshtml", viewModel);
                        }
                        employee = new ApplicationUser
                        {
                            UserName = viewModel.UserName,
                            Email = viewModel.Email,
                            PhoneNumber = viewModel.PhoneNumber,
                            Name = viewModel.Name,
                            Gender = viewModel.Gender
                        };
                        var res = await _userManager.CreateAsync(employee, viewModel.Password);
                        if (!res.Succeeded)
                        {
                            foreach (var error in res.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }

                            viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
                            return View("~/Views/Admin/Employees.cshtml", viewModel);
                        }
                    
                        await _userManager.AddToRoleAsync(employee, "Employee");
                        return RedirectToAction(nameof(GetEmployees));
                        // return NotFound();
                    }

                    employee.UserName = viewModel.UserName;
                    employee.Email = viewModel.Email;
                    employee.PhoneNumber = viewModel.PhoneNumber;
                    employee.Name = viewModel.Name;
                    employee.Gender = viewModel.Gender;

                    var result = await _userManager.UpdateAsync(employee);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
                        return View("~/Views/Admin/Employees.cshtml", viewModel);
                    }

                    // Update the password if provided
                    if (!string.IsNullOrEmpty(viewModel.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(employee);
                        var passwordResult = await _userManager.ResetPasswordAsync(employee, token, viewModel.Password);
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
                }
                return RedirectToAction(nameof(GetEmployees));
            }
            // Log validation errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogError($"Property: {state.Key} Error: {error.ErrorMessage}");
                }
            }
            viewModel.Employees = await _userManager.Users.Where(u => u.UserName != "Admin").ToListAsync();
            return View("~/Views/Admin/Employees.cshtml", viewModel);
        }
    }
}

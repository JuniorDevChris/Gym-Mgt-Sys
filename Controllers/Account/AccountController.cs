using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Data;
using Gym_Mgt_System.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Gym_Mgt_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("EmployeeDashboard", "Employee");
                }
            }
            return View("~/Views/Accounts/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.Now)
                    {
                        ModelState.AddModelError("", "Account is locked out.");
                        return View(model);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        
                        // Redirect to dashboard based on username
                        if (model.UserName.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        {
                            return RedirectToAction("AdminDashboard", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("EmployeeDashboard", "Employees");
                        }
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);
                        if (await _userManager.IsLockedOutAsync(user))
                        {
                            ModelState.AddModelError("", "Account is locked out.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid login attempt.");
                        }
                    }
                }
                else
                {
                    return View("~/Views/Accounts/Login.cshtml");
                    // ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Gym_CliMgt_System.Models;

namespace Gym_Mgt_System.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // For now, just redirect to Home/Index for demonstration purposes
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}

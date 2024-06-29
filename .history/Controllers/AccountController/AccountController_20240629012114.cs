using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Models;

namespace Gym_Mgt_System.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

    }
}
